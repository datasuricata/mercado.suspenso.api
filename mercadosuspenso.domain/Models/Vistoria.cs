using mercadosuspenso.domain.Enums;
using mercadosuspenso.domain.Extensions;
using System;

namespace mercadosuspenso.domain.Models
{
    public class Vistoria : Entity
    {
        public Vistoria(Doacao doacao)
        {
            Hash = IRandomExtension.RandomLetter(8);
            Processamento = DateTime.UtcNow;
            Doacao = doacao;
            Status = default;
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

        public void Resgatar(string distribuidorId)
        {
            Hash = IRandomExtension.RandomLetter(8);
            Status = VistoriaStatus.Resgate;
            DistribudidorId = distribuidorId;
        }
        public void Retirar(string participanteId)
        {
            Status = VistoriaStatus.Retirada;
            ParticipanteId = participanteId;
        }
    }
}