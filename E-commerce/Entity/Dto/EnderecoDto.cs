namespace E_commerce.Entity.Dto;

public class EnderecoDto
{
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro {get; set;}
        public string Cep  { get; set; }
        public string Cidade  { get; set; }
        public string Estado   { get; set; }
        public string Tipo   { get; set; }
        public string? Complemento  { get; set; }
}