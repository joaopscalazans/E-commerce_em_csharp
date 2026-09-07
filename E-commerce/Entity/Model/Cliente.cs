namespace E_commerce.Entity.Model;

public class Cliente
{
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public Usuario Usuario { get; set; }
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