using E_commerce.Entity.Model;

namespace E_commerce.Entity.Dto;

public class PedidoRegistroDto
{
    public int IdEnderecoEntrega { get; set; }
    public List<ItemPedidoRegistroDto> Itens { get; set; } = new();
}