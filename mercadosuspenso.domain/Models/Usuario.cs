using mercadosuspenso.domain.Enums;

namespace mercadosuspenso.domain.Models
{
    public class Usuario : Entity
    {
        public Usuario(string email, string senha)
        {
            Email = email;
            Senha = senha;
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