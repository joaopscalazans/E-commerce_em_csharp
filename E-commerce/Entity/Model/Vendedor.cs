using System.Text.Json.Serialization;

namespace E_commerce.Entity.Model;

public class Vendedor
{

    public int Id { get; private set; }
    public int IdUsuario { get; private set; }
    public Usuario Usuario { get; set; } = null!;
    public string? Cnpj { get; set; }
    public string? Cpf { get; set; }
    public string NomeLoja { get; set; }
    
    [JsonIgnore]
    public ICollection<Produto> Produtos { get; set; }
    
    public Vendedor()
    {}

    public Vendedor(int idUsuario, string cnpj, string cpf, string nomeLoja)
    {
        IdUsuario = idUsuario;
        Cnpj = cnpj;
        Cpf = cpf;
        NomeLoja = nomeLoja;
    }
}