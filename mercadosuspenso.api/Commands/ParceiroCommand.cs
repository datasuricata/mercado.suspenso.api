namespace mercadosuspenso.api.Commands
{
    public class ParceiroCommand
    {
        public string RazaoSocial { get; set; }
        public string Representante { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Aceite { get; set; }
    }
}