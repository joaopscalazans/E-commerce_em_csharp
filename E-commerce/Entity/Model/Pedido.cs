using System.Text.Json.Serialization;


namespace E_commerce.Entity.Model;

public class Pedido
{
    public int Id { get; private set; }
    public int IdCliente { get; private set; }
    public Cliente Cliente { get; set; } = null!;
    public int IdEndereco { get; private set; }
    public Endereco Endereco { get; set; } = null!;
    public decimal ValorTotal { get; private set; }
    public DateTime DataPedido { get; private set; }
    
    [JsonIgnore]
    public ICollection<ItemPedido> Itens { get; private set; } = new List<ItemPedido>();

    public Pedido()
    {}

    public Pedido(int idEndereco,int idCliente)
    {
        IdCliente = idCliente;
        IdEndereco = idEndereco;
        ValorTotal = 0;
        DataPedido = DateTime.UtcNow;
    }

    public void AdicionarItemPedido(int  idProduto, int quantidade, decimal precoUnitario)
    {
        Itens.Add(new ItemPedido(idProduto, quantidade, precoUnitario));
        ValorTotal += (precoUnitario * quantidade);
    }
    
    
}