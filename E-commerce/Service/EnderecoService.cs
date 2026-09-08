using E_commerce.Entity.Dto;
using E_commerce.Entity.Model;
using E_commerce.Repository.Interface;
using E_commerce.Service.Interface;

namespace E_commerce.Service;


public class EnderecoService : IEnderecoService
{
  private readonly IEnderecoRepository _repository;
    private readonly IUsuarioRepository _usuarioRepository;

    public EnderecoService(IEnderecoRepository repository, IUsuarioRepository usuarioRepository)
    {
        _repository = repository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<(Endereco? endereco, string? mensagem)> ObterPorUsuario(int idUsuario, int idEndereco)
    {
        if (idUsuario <= 0 || idEndereco <= 0)
            return (null, "IDs inválidos");

        var endereco = await _repository.ObterPorUsuarioAsync(idUsuario, idEndereco);
        return endereco != null 
            ? (endereco, null) 
            : (null, "Endereço não encontrado para este usuário");
    }

    public async Task<(List<Endereco> enderecos, string? mensagem)> ObterTodosPorUsuario(int idUsuario)
    {
        if (idUsuario <= 0 || !await _usuarioRepository.ExistePorId(idUsuario))
            return (new List<Endereco>(), "ID do usuário inválido");

        var enderecos = await _repository.ObterTodosPorUsuarioAsync(idUsuario);
        return (enderecos, null);
    }

    public async Task<(bool Registrou, string mensagem)> Registrar(int idUsuario, EnderecoDto dto)
    {
        if (idUsuario <= 0 || !await _usuarioRepository.ExistePorId(idUsuario))
            return (false, "Usuário não encontrado");

        if (dto == null)
            return (false, "Dados do endereço não podem ser nulos");

        var (valido, erro) = ValidarCampos(dto);
        if (!valido) return (false, erro!);

        var endereco = new Endereco(
            idUsuario, 
            dto.Rua, 
            dto.Numero, 
            dto.Bairro, 
            dto.Cep, 
            dto.Cidade, 
            dto.Estado, 
            dto.Tipo, 
            dto.Complemento
        );

        await _repository.RegistrarAsync(endereco);
        return (true, "Endereço registrado com sucesso");
    }

    public async Task<(bool Atualizou, string mensagem)> Atualizar(int idUsuario, int idEndereco, EnderecoDto dto)
    {
        if (idUsuario <= 0 || idEndereco <= 0)
            return (false, "IDs inválidos");

        if (dto == null)
            return (false, "Dados do endereço não podem ser nulos");
        
        var enderecoExistente = await _repository.ObterPorUsuarioAsync(idUsuario, idEndereco);
        if (enderecoExistente == null)
            return (false, "Endereço não encontrado para este usuário");

        var (valido, erro) = ValidarCampos(dto);
        if (!valido) return (false, erro!);
        
        enderecoExistente.Rua = dto.Rua;
        enderecoExistente.Numero = dto.Numero;
        enderecoExistente.Bairro = dto.Bairro;
        enderecoExistente.Cep = dto.Cep;
        enderecoExistente.Cidade = dto.Cidade;
        enderecoExistente.Estado = dto.Estado;
        enderecoExistente.Tipo = dto.Tipo;
        enderecoExistente.Complemento = dto.Complemento;

        await _repository.AtualizarAsync(enderecoExistente);
        return (true, "Endereço atualizado com sucesso");
    }
    public async Task<(bool Removeu, string mensagem)> Remover(int idUsuario, int idEndereco)
    {
        if (idUsuario <= 0 || idEndereco <= 0)
            return (false, "IDs inválidos");
        var usuario = await _usuarioRepository.ObterPorId(idUsuario);
        if (usuario == null) return (false, "Usuario invalido");
        var endereco = await _repository.ObterPorUsuarioAsync(usuario.Id, idEndereco);
        if (endereco == null)
            return (false, "Endereço não encontrado para este usuário");

        await _repository.RemoverAsync(endereco);
        return (true, "Endereço removido com sucesso");
    }

    private (bool Valido, string? Erro) ValidarCampos(EnderecoDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Rua)) return (false, "Rua é obrigatória");
        if (string.IsNullOrWhiteSpace(dto.Numero)) return (false, "Número é obrigatório");
        if (string.IsNullOrWhiteSpace(dto.Bairro)) return (false, "Bairro é obrigatório");
        if (string.IsNullOrWhiteSpace(dto.Cep)) return (false, "CEP é obrigatório");
        if (string.IsNullOrWhiteSpace(dto.Cidade)) return (false, "Cidade é obrigatória");
        if (string.IsNullOrWhiteSpace(dto.Estado)) return (false, "Estado é obrigatório");
        if (string.IsNullOrWhiteSpace(dto.Tipo)) return (false, "Tipo de endereço é obrigatório");
        if (!ValidarCep(dto.Cep)) return (false, "CEP inválido");

        return (true, null);
    }

    private bool ValidarCep(string cep)
    {
        if (string.IsNullOrWhiteSpace(cep)) return false;
        var cepLimpo = cep.Replace("-", "").Replace(".", "").Trim();
        return cepLimpo.Length == 8 && cepLimpo.All(char.IsDigit);
    }
}