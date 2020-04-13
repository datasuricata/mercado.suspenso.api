using mercadosuspenso.domain.Enums;
using mercadosuspenso.domain.Extensions;

namespace mercadosuspenso.domain.Models
{
    public class Usuario : Entity
    {
        public Usuario(string email, string senha)
        {
            Senha = senha.EncryptToMD5();
            Email = email;
        }

        protected Usuario()
        {
        }

        public string Email { get; set; }
        public string Senha { get; set; }
        public UsuarioTipo Tipo { get; set; }

        public void DefinirTipo(UsuarioTipo tipo) => Tipo = tipo;
    }
}