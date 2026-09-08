using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace E_commerce.Entity.Model;

public class Pedido
{
    public int Id { get; private set; }
    public int IdCliente { get; private set; }
    public Cliente Cliente { get; set; } = null!;
    public int IdEndereco { get; private set; }
    public Endereco Endereco { get; set; } = null!;
    

    public DateTime DataPedido { get; private set; }
    
    [JsonIgnore]
    public ICollection<ItemPedido> Itens { get; private set; } = new List<ItemPedido>();
    [NotMapped]
    public decimal ValorTotal => Itens.Sum(item => item.PrecoUnitario * item.Quantidade);

    public Pedido()
    {}

    public Pedido(int idEndereco,int idCliente)
    {
        IdCliente = idCliente;
        IdEndereco = idEndereco;
        DataPedido = DateTime.UtcNow;
    }
    
    
}