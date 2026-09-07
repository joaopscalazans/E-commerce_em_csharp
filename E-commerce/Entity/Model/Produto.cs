namespace E_commerce.Entity.Model;

public class Produto
{
    public int Id { get; private set; }
    public int IdVendedor { get; private set; }
    public Vendedor Vendedor { get; private set; }
    public string Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    
    public Produto(){}

    public Produto(int IdVendedor, string Nome, string? Descricao, decimal Preco)
    {
        this.IdVendedor = IdVendedor;
        this.Nome = Nome;
        this.Descricao = Descricao;
        this.Preco = Preco;
    }
}