using mercadosuspenso.domain.Exceptions;

namespace mercadosuspenso.domain.Models
{
    public class Endereco : Entity
    {
        public Endereco(string cep, string logradouro, string numero, string complemento, string bairro, string cidade, string estado)
        {
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;

            Validar();
        }

        protected Endereco()
        {
        }

        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }


        public void Validar()
        {
            Assert Quando = DomainException.Validate;

            Quando(string.IsNullOrEmpty(Cep), "Informe o Cep");
            Quando(string.IsNullOrEmpty(Logradouro), "Informe o endereço do logradouro");
            Quando(string.IsNullOrEmpty(Numero), "Numero deve ser informado");
            Quando(string.IsNullOrEmpty(Bairro), "Bairro deve ser informado");
            Quando(string.IsNullOrEmpty(Cidade), "Cidade é obrigatório");
            Quando(string.IsNullOrEmpty(Estado), "Estadó é obrigatório");
        }
    }
}