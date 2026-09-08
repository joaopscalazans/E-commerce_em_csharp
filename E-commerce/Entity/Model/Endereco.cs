using System.Text.Json.Serialization;

namespace E_commerce.Entity.Model;

public class Endereco
{
    public int Id { get; set; }
    public int IdUsuario { get; private set; }
    [JsonIgnore]
    public Usuario Usuario { get; set; } = null!;
    public string Rua  { get; set; } = string.Empty;
    public string Numero { get; set; }  = string.Empty;
    public string Bairro { get; set; }  = string.Empty;
    public string? Complemento { get; set; } = null;
    public string Cep { get; set; }   = string.Empty;
    public string Cidade { get; set; }   = string.Empty;
    public string Estado { get; set; }   = string.Empty;
    public string? Tipo { get; set; }   = null;
    
    public Endereco(){}

    public Endereco(int idUsuario,
        string rua,
        string numero,
        string bairro,
        string cep,
        string cidade,
        string estado,
        string tipo,
        string? complemento)
    {
        IdUsuario = idUsuario;
        Rua = rua;
        Numero = numero;
        Bairro = bairro;
        Cep = cep;
        Cidade = cidade;
        Estado = estado;
        Tipo = tipo;
        Complemento = complemento;
    }
    
   
}