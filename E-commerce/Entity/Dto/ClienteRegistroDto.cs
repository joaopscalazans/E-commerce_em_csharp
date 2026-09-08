namespace E_commerce.Entity.Dto;

public class ClienteRegistroDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string CPF { get; set; }
    public DateTime DataNascimento { get; set; }
}