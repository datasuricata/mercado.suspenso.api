using mercadosuspenso.domain.Models;

namespace mercadosuspenso.domain.Dtos
{
    public class VinculoDto
    {
        public string Id { get; set; }
        public (string Nome, string Id) Distribuidor { get; set; }
        public (string Nome, string Id) Varejista { get; set; }
        public bool Ativo { get; set; }

        public static VinculoDto From(Vinculo from)
        {
            if (from != null)
            {
                return new VinculoDto
                {
                    Id = from.Id,
                    Ativo = from.Ativo,
                    Distribuidor = (from.Distribuidor?.RazaoSocial, from.DistribuidorId),
                    Varejista = (from.Varejista?.RazaoSocial, from.VarejistaId),
                };
            }

            return null;
        }
    }
}