namespace mercadosuspenso.domain.Models.External
{
    public class BolsaFamilia
    {
        public string DataMesCompetencia { get; set; }
        public string DataMesReferencia { get; set; }
        public int Id { get; set; }
        public int QuantidadeDependentes { get; set; }
        public TitularBolsaFamilia TitularBolsaFamilia { get; set; }
        public int Valor { get; set; }
    }
}