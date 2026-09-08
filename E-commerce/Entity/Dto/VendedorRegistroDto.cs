namespace E_commerce.Entity.Dto;

public class VendedorRegistroDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string? CPF { get; set; }
    public string? Cnpj { get; set; }
    public string NomeLoja { get; set; }
}