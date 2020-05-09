using System;
using System.ComponentModel.DataAnnotations;

namespace mercadosuspenso.domain.Models
{
    public abstract class Entity
    {
        protected delegate void Assert(bool error, string message);

        protected Entity()
        {
            Id = Guid.NewGuid().ToString().Replace("-", "");
            CriadoEm = DateTimeOffset.UtcNow;
            Ativo = true;
        }

        [Key]
        [MaxLength(32)]
        public string Id { get; set; }
        public DateTimeOffset CriadoEm { get; set; }
        public DateTimeOffset? AtualizadoEm { get; set; }
        public bool Ativo { get; set; }
    }
}