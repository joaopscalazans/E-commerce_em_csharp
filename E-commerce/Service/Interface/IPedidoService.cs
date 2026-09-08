using E_commerce.Entity.Dto;
using E_commerce.Entity.Model;

namespace E_commerce.Service.Interface;

public interface IPedidoService
{
    Task<(IEnumerable<Pedido>? Pedidos, string? Mensagem)> ObterTodosPorCliente(int idCliente);
    Task<(Pedido? Pedido, string? Mensagem)> ObterPorClienteEPedido(int idCliente, int idPedido);
    Task<(bool Registrou, string Mensagem)> Registrar(int idCliente, PedidoRegistroDto dto);
    Task<(bool Cancelou, string Mensagem)> CancelarPedido(int idCliente, int idPedido);
}