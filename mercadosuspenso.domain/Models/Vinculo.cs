namespace mercadosuspenso.domain.Models
{
    public class Vinculo : Entity
    {
        public Vinculo(string distribuidorId, string varejistaId)
        {
            DistribuidorId = distribuidorId;
            VarejistaId = varejistaId;
        }

        public Vinculo(Distribuidor distribuidor, Varejista varejista)
        {
            Distribuidor = distribuidor;
            Varejista = varejista;
        }

        protected Vinculo()
        {
        }

        public string DistribuidorId { get; set; }
        public Distribuidor Distribuidor { get; set; }

        public string VarejistaId { get; set; }
        public Varejista Varejista { get; set; }

        public string UsuarioId { get; set; }
    }
}
