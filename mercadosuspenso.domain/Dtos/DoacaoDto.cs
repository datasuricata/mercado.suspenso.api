using mercadosuspenso.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mercadosuspenso.domain.Dtos
{
    public class DoacaoDto
    {
        public string Id { get; set; }
        public string VarejistaId { get; set; }
        public IEnumerable<ProdutoDto> Produtos { get; set; }

        public static DoacaoDto From(Doacao from)
        {
            if (from != null)
            {
                return new DoacaoDto
                {
                    Id = from.Id,
                    VarejistaId = from.VarejistaId,
                    Produtos = from.DoacaoProdutos.Select(a => ProdutoDto.From(a.Produto)),
                };
            }

            return null;
        }
    }
}
