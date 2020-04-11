namespace mercadosuspenso.domain.Models
{
    public class DoacaoProduto
    {
        protected DoacaoProduto()
        {
        }

        public string DoacaoId { get; set; }
        public Doacao Doacao { get; set; }

        public string ProdutoId { get; set; }
        public Produto Produto { get; set; }
    }
}
