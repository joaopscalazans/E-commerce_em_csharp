namespace E_commerce.Entity.Model;

public class ItemPedido
{
    public int Id { get; private set; }
    public int IdPedido { get; private set; }
    public Pedido Pedido { get; set; }
    public int IdProduto { get; private set; }
    public Produto Produto { get; set; }
    public int Quantidade { get;  set; }
    public decimal PrecoUnitario { get;  private set;}

    public ItemPedido(){}
    public ItemPedido(int idProduto, int quantidade, decimal precoUnitario)
    {
        IdProduto = idProduto;
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
    }
}