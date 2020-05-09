using mercadosuspenso.domain.Exceptions;
using System.Collections.Generic;

namespace mercadosuspenso.domain.Models
{
    public class Doacao : Entity
    {
        public Doacao(string cpf, string detalhes, string varejistaId)
        {
            Cpf = cpf;
            Detalhes = detalhes;
            VarejistaId = varejistaId;

            Validar();
        }

        protected Doacao()
        {
        }

        public string Cpf { get; set; }
        public string Detalhes { get; set; }
        public string VarejistaId { get; set; }

        public ICollection<DoacaoProduto> DoacaoProdutos { get; set; } = new HashSet<DoacaoProduto>();

        public void Validar()
        {
            Assert Quando = DomainException.Validate;
            Quando(string.IsNullOrEmpty(VarejistaId), "Varejista deve ser informado");
        }
    }
}