using E_commerce.Entity.Model;

namespace E_commerce.Service.Interface;

public interface IItemPedidoService
{
    Task<(bool Registrou,string? mensagem)> Registar(ItemPedido itemPedido);
    Task<(bool Atualizou,string? mensagem)> Atualizar(int id,ItemPedido itemPedido);
    Task<(bool Removeu,string? mensagem)> Remover(int id);
}