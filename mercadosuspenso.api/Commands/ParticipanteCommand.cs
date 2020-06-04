namespace mercadosuspenso.api.Commands
{
    public class ParticipanteCommand
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public EnderecoCommand Endereco { get; set; }
    }
}
