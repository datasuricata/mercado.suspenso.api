using mercadosuspenso.domain.Enums;
using mercadosuspenso.domain.Exceptions;

namespace mercadosuspenso.domain.Models
{
    public class Distribuidor : Entity
    {
        public Distribuidor(string razaoSocial, string nome, string cnpj, string telefone)
        {
            Telefone = telefone;
            RazaoSocial = razaoSocial;
            Representante = nome;
            Cnpj = cnpj;
            Status = default;
        }

        protected Distribuidor()
        {
        }

        public string RazaoSocial { get; set; }
        public string Representante { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }
        public RegistroStatus Status { get; set; }

        public string EnderecoId { get; set; }
        public Endereco Endereco { get; set; }

        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public void Aprova() => Status = RegistroStatus.Aproved;

        public void Recusa() => Status = RegistroStatus.Refused;

        public void Validate()
        {
            Assert When = Domain.Validate;
            
            When(string.IsNullOrEmpty(Representante), "Nome do representante legal é obrigatório");
            When(string.IsNullOrEmpty(Cnpj), "Cnpj deve ser informado");
            When(string.IsNullOrEmpty(RazaoSocial), "Razao Social deve ser informada");
            When(Cnpj.Length != 14, "Cnpj inválido");
        }
    }
}