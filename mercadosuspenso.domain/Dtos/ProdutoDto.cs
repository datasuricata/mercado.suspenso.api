using mercadosuspenso.domain.Models;

namespace mercadosuspenso.domain.Dtos
{
    public class ProdutoDto
    {
        public string Id { get; set; }
        public decimal Quantidade { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }

        public static ProdutoDto From(Produto from)
        {
            if (from != null)
            {
                return new ProdutoDto
                {
                    Id = from.Id,
                    Codigo = from.Codigo,
                    Nome = from.Nome,
                };
            }

            return null;
        }
    }
}
