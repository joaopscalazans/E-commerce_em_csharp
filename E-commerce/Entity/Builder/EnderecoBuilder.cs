using E_commerce.Entity.Model;

namespace E_commerce.Entity.Builder;


    public class EnderecoBuilder
    {
        private int IdUsuario;
        private string _rua = string.Empty;
        private string _numero = string.Empty;
        private string _bairro = string.Empty;
        private string? _complemento;
        private string _cep = string.Empty;
        private string _cidade = string.Empty;
        private string _estado = string.Empty;
        private string _tipo = string.Empty;

        public EnderecoBuilder ComUsuario(int idUsuario)
        {
            IdUsuario = idUsuario;
            return this;
        }

        public EnderecoBuilder ComLogradouro(string rua, string numero, string bairro, string? complemento = null)
        {
            _rua = rua;
            _numero = numero;
            _bairro = bairro;
            _complemento = complemento;
            return this;
        }

        public EnderecoBuilder ComLocalidade(string cep, string cidade, string estado)
        {
            _cep = cep;
            _cidade = cidade;
            _estado = estado;
            return this;
        }

        public EnderecoBuilder DoTipo(string tipo)
        {
            _tipo = tipo;
            return this;
        }

        public Endereco Build()
        {
            if (string.IsNullOrWhiteSpace(_cep))
                throw new InvalidOperationException("O CEP é obrigatório para construir o endereço.");

            return new Endereco(IdUsuario, _rua, _numero, _bairro, _cep, _cidade, _estado, _tipo, _complemento);
        }
    }
