using E_commerce.Entity.Enum;

namespace E_commerce.Entity.Model;

public class Usuario
{
    public int Id { get; private set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; private set; }
    public TipoUsuario TipoUsuario {get; private set;}

    public Usuario(){}
    public Usuario(string nome, string email,string senha, TipoUsuario tipoUsuario)
    {
        Nome = nome;
        Email = email;
        TipoUsuario = tipoUsuario;
        Senha = senha;
    }
}