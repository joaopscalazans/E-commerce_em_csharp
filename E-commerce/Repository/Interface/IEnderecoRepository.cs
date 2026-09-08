using E_commerce.Entity.Model;

namespace E_commerce.Repository.Interface;

public interface IEnderecoRepository
{
    Task<Endereco?> ObterPorUsuarioAsync(int idUsuario, int idEndereco);
    Task<List<Endereco>> ObterTodosPorUsuarioAsync(int idUsuario);
    Task RegistrarAsync(Endereco endereco);
    Task AtualizarAsync(Endereco endereco);
    Task RemoverAsync(Endereco endereco);
    Task<bool> PertenceAoCliente(int idUsuario, int idEndereco);
}