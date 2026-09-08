using E_commerce.Entity.Model;

namespace E_commerce.Repository.Interface;

public interface IItemPedidoRepository
{
    Task RegistarAsync(ItemPedido itemPedido);
    Task AtualizarAsync(ItemPedido itemPedido);
    Task RemoverAsync(ItemPedido itemPedido);
}