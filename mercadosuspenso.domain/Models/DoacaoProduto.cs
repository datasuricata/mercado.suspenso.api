namespace mercadosuspenso.domain.Models
{
    public class DoacaoProduto
    {
        public DoacaoProduto(string doacaoId, string produtoId)
        {
            DoacaoId = doacaoId;
            ProdutoId = produtoId;
        }

        public DoacaoProduto(Doacao doacao, Produto produto)
        {
            Doacao = doacao;
            Produto = produto;
        }

        protected DoacaoProduto()
        {
        }

        public string DoacaoId { get; set; }
        public Doacao Doacao { get; set; }

        public string ProdutoId { get; set; }
        public Produto Produto { get; set; }
    }
}
