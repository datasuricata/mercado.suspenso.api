using mercadosuspenso.domain.Enums;
using System;

namespace mercadosuspenso.domain.Models
{
    public class Vistoria : Entity
    {
        public Vistoria(string hash, DateTime processamento, VistoriaStatus status, string distribudidorId)
        {
            Hash = hash;
            Processamento = processamento;
            Status = status;
            DistribudidorId = distribudidorId;
        }

        protected Vistoria()
        {

        }

        public string Hash { get; set; }
        public DateTime Processamento { get; set; }
        public VistoriaStatus Status { get; set; }

        public string DoacaoId { get; set; }
        public Doacao Doacao { get; set; }

        public string DistribudidorId { get; set; }
        public Distribuidor Distribuidor { get; set; }

        public string ParticipanteId { get; set; }
        public Participante Participante { get; set; }
    }
}