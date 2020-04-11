using mercadosuspenso.domain.Enums;
using mercadosuspenso.domain.Exceptions;

namespace mercadosuspenso.domain.Models
{
    public class Participante : Entity
    {
        public Participante(string nome, string cpf, string rg, string telefone)
        {
            Telefone = telefone;
            Nome = nome;
            Cpf = cpf;
            Rg = rg;
            Status = default;

            Validate();
        }

        protected Participante()
        {
        }

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Telefone { get; set; }
        public RegistroStatus Status { get; set; }

        public string EnderecoId { get; set; }
        public Endereco Endereco { get; set; }

        public void Aprova() => Status = RegistroStatus.Aproved;

        public void Recusa() => Status = RegistroStatus.Refused;

        public void Validate()
        {
            Assert When = Domain.Validate;

            When(string.IsNullOrEmpty(Nome), "Nome é obrigatório");
            When(string.IsNullOrEmpty(Cpf), "Cpf deve ser informado");
            When(Cpf.Length != 11, "Cpf inválido");
            When(string.IsNullOrEmpty(Rg), "Rg inválido");
        }
    }
}