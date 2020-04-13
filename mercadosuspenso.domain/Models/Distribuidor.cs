using mercadosuspenso.domain.Enums;
using mercadosuspenso.domain.Exceptions;
using mercadosuspenso.domain.Extensions;

namespace mercadosuspenso.domain.Models
{
    public class Distribuidor : Entity
    {
        public Distribuidor(string razaoSocial, string representante, string cnpj, string telefone)
        {
            Cnpj = cnpj.CleanFormat();
            Telefone = telefone;
            RazaoSocial = razaoSocial;
            Representante = representante;
            Status = default;

            Validar();
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

        public void Aprovar() => Status = RegistroStatus.Aproved;

        public void Recusar() => Status = RegistroStatus.Refused;

        public void Validar()
        {
            Assert Quando = Domain.Validate;
            
            Quando(string.IsNullOrEmpty(Representante), "Nome do representante legal é obrigatório");
            Quando(string.IsNullOrEmpty(Cnpj), "Cnpj deve ser informado");
            Quando(string.IsNullOrEmpty(RazaoSocial), "Razao Social deve ser informada");
            Quando(Cnpj.Length != 14, "Cnpj inválido");
        }
    }
}