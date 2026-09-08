using E_commerce.Entity.Model;

namespace E_commerce.Repository.Interface;

public interface IPedidoRepository
{
    Task<List<Pedido>> ObterTodosPorClienteAsync(int idCliente);
    Task<Pedido> ObterPorIdAsync(int idCliente,int idPedido);
    Task RegistrarAsync(Pedido pedido);
    Task RemoverAsync(Pedido pedido);

}