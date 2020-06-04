namespace mercadosuspenso.domain.Models
{
    public class Aceite : Entity
    {
        public Aceite(string usuarioId, string documento)
        {
            UsuarioId = usuarioId;
            Documento = documento;
        }

        protected Aceite()
        {
        }

        public string UsuarioId { get; set; }
        public string Documento { get; set; }
    }
}
