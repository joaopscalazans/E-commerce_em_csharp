namespace E_commerce.Entity.Model;

public class Cliente
{
    public int Id { get; set; }
    public int IdUsuario { get; private set; }
    public Usuario Usuario { get; set; } = null!;
    public string Cpf { get; set; }
    public DateTime DataNascimento { get; set; }
    
    public Cliente ()
    {
    }

    public Cliente(Usuario usuario, string cpf, DateTime dataNascimento)
    {
        IdUsuario = usuario.Id;
        Usuario = usuario;
        Cpf = cpf;
        DataNascimento = dataNascimento;      
    }
}