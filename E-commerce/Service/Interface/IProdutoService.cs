using E_commerce.Entity.Dto;
using E_commerce.Entity.Model;

namespace E_commerce.Service.Interface;

public interface IProdutoService
{
    Task<(Produto? produto, string? mensagem)> ObterPorId(int id);
    Task<(List<Produto> produtos, string? mensagem)> ObterTodosPorVendedor(int idVendedor);
    Task<(bool Registrou,string? mensagem)> Registrar(int idVendedor,ProdutoDto dto);
    Task<(bool Atualizou,string mensagem)> Atualizar(int id,ProdutoDto produto);
    Task<(bool Removeu ,string mensagem)> RemoverPorId(int id);
}