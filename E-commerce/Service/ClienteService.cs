using E_commerce.Entity.Dto;
using E_commerce.Entity.Enum;
using E_commerce.Entity.Model;
using E_commerce.Infa;
using E_commerce.Repository;
using E_commerce.Repository.Interface;
using E_commerce.Service.Interface;

namespace E_commerce.Service;

public class ClienteService : IClienteService
{
    
    private readonly IClienteRepository _repository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IValidadorCpfAsyncService _validadorCpfAsyncService;

    public ClienteService(IClienteRepository repository, 
        IValidadorCpfAsyncService validadorCpfAsyncService,
        IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
        _repository = repository;
        _validadorCpfAsyncService = validadorCpfAsyncService;
    }
    
    public async Task<(bool Registrou,string mensagem)> Registar(ClienteRegistroDto dto)
    {
        if (string.IsNullOrEmpty(dto.Nome) || string.IsNullOrWhiteSpace(dto.Email)) return (false,"Campo Obrigatorio: Nome");
        
        if(!dto.Email.Contains("@") && !dto.Email.Contains(".")) return (false,"Campo Invalido: Email");
        
        if(dto.DataNascimento > DateTime.Now) return (false ,"Campo Invalido: data nascimento");
        
        if(await _usuarioRepository.ExistePorEmail(dto.Email))  return (false ,"Email já cadastrado");
        
        if(!await _validadorCpfAsyncService.ValidarCpfAsync(dto.CPF)) return (false ,"Cpf invalido");
        
        if(await _repository.ExistePorCpf(dto.CPF)) return (false ,"Cpf já cadastrado");

        await _repository.RegistarAsync(
            new Cliente(
                new Usuario(
                    dto.Nome,
                    dto.Email,
                    dto.Senha,
                    TipoUsuario.CLIENTE),
                dto.CPF,
                DateTime.SpecifyKind(dto.DataNascimento, DateTimeKind.Utc)
            ));
        return (true, "cliente cadastrado com sucesso");
    }

    public async Task<(bool Atualizou, string mensagem)> Atualizar(int id, ClienteAlterarDto dto)
    {
       var cliente = await  _repository.ObterPorIdAsync(id);
       if(cliente is null) return (false,"Usuario não encontrado");
       
       if (string.IsNullOrEmpty(dto.Nome) || string.IsNullOrWhiteSpace(dto.Email)) return (false,"Campo Obrigatorio: Nome");
        
       if(!dto.Email.Contains("@") && !dto.Email.Contains(".")) return (false,"Campo Invalido: Email");
        
       if(dto.DataNascimento > DateTime.Now) return (false,"Campo Invalido: data nascimento");
       
       if(!dto.Email.Equals(cliente.Usuario.Email))
           if(!_usuarioRepository.ExistePorEmail(dto.Email).Result) return (false,"Email já cadastrado");
       
       cliente.Usuario.Nome = dto.Nome;
       cliente.Usuario.Email = dto.Email;
       cliente.DataNascimento = dto.DataNascimento;
       
       await _repository.AtualizarAsync(cliente);
       
       return (true, "cliente atualizado com sucesso");
    }

    public async Task<(bool,string)> RemoverPorId(int id)
    {
        var cliente = await  _repository.ObterPorIdAsync(id); 
        if(cliente is null) return (false,"Usuario não encontrado");
        
       await _repository.RemoverAsync(cliente);
    return (true, "cliente removido com sucesso");
    }

    public async Task<(Cliente? cliente, string mensagem)> ObterPorId(int id)
    {
        var cliente = await _repository.ObterPorIdAsync(id);
        return cliente != null ? (cliente, null):(null, "usuario não encontrado")  ;
    }

}