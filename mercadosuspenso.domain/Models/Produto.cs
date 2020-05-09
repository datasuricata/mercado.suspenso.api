using System.Collections.Generic;

namespace mercadosuspenso.domain.Models
{
    public class Produto : Entity
    {
        public Produto(decimal quantidade, string nome, string codigo)
        {
            Quantidade = quantidade;
            Nome = nome;
            Codigo = codigo;
        }

        protected Produto()
        {
        }

        public decimal Quantidade { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }

        public ICollection<DoacaoProduto> DoacaoProdutos { get; set; } = new HashSet<DoacaoProduto>();
    }
}