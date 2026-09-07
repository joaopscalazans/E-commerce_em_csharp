using E_commerce.Entity.Enum;

namespace E_commerce.Entity.Model;

public class Usuario
{
    public int Id { get; private set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; private set; }
    public TipoUsuario TipoUsuario {get; private set;}
    
}