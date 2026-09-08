using E_commerce.Entity.Dto;
using E_commerce.Entity.Enum;
using E_commerce.Entity.Model;
using E_commerce.Repository.Interface;
using E_commerce.Service.Interface;

namespace E_commerce.Service;

public class VendedorService : IVendedorService
{
      
    private readonly IVendedorRepository _repository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IValidadorCpfAsyncService _validadorCpfAsyncService;
    private readonly IValidadorCnpjAsyncService  _validadorCnpjAsyncService;

    public VendedorService(IVendedorRepository repository, 
        IValidadorCpfAsyncService validadorCpfAsyncService,
        IUsuarioRepository  usuarioRepository,
        IValidadorCnpjAsyncService validadorCnpjAsyncService)
    {
        _usuarioRepository = usuarioRepository;
        _repository = repository;
        _validadorCpfAsyncService = validadorCpfAsyncService;
        _validadorCnpjAsyncService = validadorCnpjAsyncService;
    }
    
    public async Task<(bool Registrou,string? mensagem)> Registar(VendedorRegistroDto dto)
    {
        if (string.IsNullOrEmpty(dto.Nome) || string.IsNullOrWhiteSpace(dto.Email)) return (false,"Campo Obrigatorio: Nome");
        
        if(!dto.Email.Contains("@") && !dto.Email.Contains(".")) return (false,"Campo Invalido: Email");
        
        if(await _usuarioRepository.ExistePorEmail(dto.Email))  return (false ,"Email já cadastrado");

        if (dto.CPF is null && dto.Cnpj is null) return (false, "A conta do tipo vendedor precisa de um cpf ou cnpj");
        
        if (dto.CPF != null)
        {
            if(!await _validadorCpfAsyncService.ValidarCpfAsync(dto.CPF)) return (false ,"Cpf invalido");
            if(await _repository.ExistePorCpf(dto.CPF)) return (false ,"Cpf já cadastrado");
        }

        if (dto.Cnpj != null)
        {
            if(!await _validadorCnpjAsyncService.ValidarCnpjAsync(dto.Cnpj)) return (false ,"Cnpj invalido");
            if(await _repository.ExistePorCnpj(dto.Cnpj)) return (false ,"Cnpj já cadastrado");
        }
        

        await _repository.RegistarAsync(
            new Vendedor(
                new Usuario(
                    dto.Nome,
                    dto.Email,
                    dto.Senha,
                    TipoUsuario.VENDEDOR),
                dto.Cnpj ?? string.Empty,
                dto.CPF ?? string.Empty,
                dto.NomeLoja));
        return (true, null);
    }

    public async Task<(bool Atualizou, string mensagem)> Atualizar(int id, VendedorAlterarDto dto)
    {
       var vendedor = await  _repository.ObterPorIdAsync(id);
       if(vendedor is null) return (false,"Usuario não encontrado");
       
       if (string.IsNullOrEmpty(dto.Nome) || string.IsNullOrWhiteSpace(dto.Email)) return (false,"Campo Obrigatorio: Nome");
        
       if(!dto.Email.Contains("@") && !dto.Email.Contains(".")) return (false,"Campo Invalido: Email");
       
       
       if(!dto.Email.Equals(vendedor.Usuario.Email))
           if(!_usuarioRepository.ExistePorEmail(dto.Email).Result) return (false,"Email já cadastrado");
       
       vendedor.Usuario.Nome = dto.Nome;
       vendedor.Usuario.Email = dto.Email;
       vendedor.NomeLoja = dto.NomeLoja;
       
       await _repository.AtualizarAsync(vendedor);
       
       return (true, "vendedor atualizado com sucesso");
    }
    

    public async Task<(bool Removeu,string mensagem)> RemoverPorId(int id)
    {
        var vendedor = await  _repository.ObterPorIdAsync(id); 
        if(vendedor is null) return (false,"Usuario não encontrado");
        
       await _repository.RemoverAsync(vendedor);
    return (true, "vendedor removido com sucesso");
    }

    public async Task<(Vendedor? vendedor, string mensagem)> ObterPorId(int id)
    {
        var vendedor = await _repository.ObterPorIdAsync(id);
        return vendedor != null ? (vendedor, null):(null, "usuario não encontrado")  ;
    }
}