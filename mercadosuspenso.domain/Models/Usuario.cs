using mercadosuspenso.domain.Enums;
using mercadosuspenso.domain.Exceptions;
using mercadosuspenso.domain.Extensions;

namespace mercadosuspenso.domain.Models
{
    public class Usuario : Entity
    {
        public Usuario(string email, string senha)
        {
            Senha = senha.EncryptToMD5();
            Email = email;

            Validar();
        }

        protected Usuario()
        {
        }

        public string Email { get; set; }
        public string Senha { get; set; }
        public UsuarioTipo Tipo { get; set; }

        public void DefinirTipo(UsuarioTipo tipo) => Tipo = tipo;

        public void Validar()
        {
            Assert Quando = Domain.Validate;

            Quando(string.IsNullOrEmpty(Email), "E-mail é obrigatório");
            Quando(!Email.Contains("@"), "E-mail inválido");
            Quando(string.IsNullOrEmpty(Senha), "Informe uma senha é obrigatória");
            Quando(Senha.Length < 6, "Senha fraca, escolha uma senha melhor");
        }
    }
}