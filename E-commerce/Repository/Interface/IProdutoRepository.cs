using E_commerce.Entity.Model;

namespace E_commerce.Repository.Interface;

public interface IProdutoRepository
{
    Task<Produto> ObterPorIdAsync(int id);
    Task<List<Produto>> ObterTodos();
    Task<List<Produto>> ObterTodosPorVendedorAsync(int idVendedor);
    Task RegistrarAsync(Produto produto);
    Task AtualizarAsync(Produto produto);
    Task RemoverAsync(Produto produto);
}