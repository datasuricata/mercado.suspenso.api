using mercadosuspenso.domain.Enums;
using mercadosuspenso.domain.Exceptions;
using mercadosuspenso.domain.Extensions;

namespace mercadosuspenso.domain.Models
{
    public class Participante : Entity
    {
        public Participante(string nome, string cpf, string rg, string telefone, string email)
        {
            Cpf = cpf.CleanFormat();
            Telefone = telefone;
            Nome = nome;
            Rg = rg;
            Email = email;
            Status = default;

            Validar();
        }

        protected Participante()
        {
        }

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public RegistroStatus Status { get; set; }

        public string EnderecoId { get; set; }
        public Endereco Endereco { get; set; }

        public void Aprovar() => Status = RegistroStatus.Aprovado;

        public void Recusar() => Status = RegistroStatus.Recusado;

        public void Validar()
        {
            Assert Quando = DomainException.Validate;

            Quando(string.IsNullOrEmpty(Nome), "Nome é obrigatório");
            Quando(string.IsNullOrEmpty(Cpf), "Cpf deve ser informado");
            Quando(Cpf.Length != 11, "Cpf inválido");
            Quando(string.IsNullOrEmpty(Rg), "Rg inválido");
        }
    }
}