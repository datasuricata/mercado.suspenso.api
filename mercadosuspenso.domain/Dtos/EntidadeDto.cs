using mercadosuspenso.domain.Models;

namespace mercadosuspenso.domain.Dtos
{
    public class EntidadeDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Telefone { get; set; }
        public string Status { get; set; }
        public string CriadoEm { get; set; }

        public static EntidadeDto From(Distribuidor from)
        {
            if (from != null)
            {
                return new EntidadeDto
                {
                    Id = from.Id,
                    CriadoEm = from.CriadoEm.ToString("dd/MM/yyyy hh:MM"),
                    Documento = from.Cnpj,
                    Nome = from.RazaoSocial,
                    Telefone = from.Telefone,
                    Status = from.Status.ToString(),
                };
            }

            return null;
        }

        public static EntidadeDto From(Varejista from)
        {
            if (from != null)
            {
                return new EntidadeDto
                {
                    Id = from.Id,
                    CriadoEm = from.CriadoEm.ToString("dd/MM/yyyy hh:MM"),
                    Documento = from.Cnpj,
                    Nome = from.RazaoSocial,
                    Telefone = from.Telefone,
                    Status = from.Status.ToString(),
                };
            }

            return null;
        }

        public static EntidadeDto From(Participante from)
        {
            if (from != null)
            {
                return new EntidadeDto
                {
                    Id = from.Id,
                    CriadoEm = from.CriadoEm.ToString("dd/MM/yyyy hh:MM"),
                    Documento = from.Cpf,
                    Nome = from.Nome,
                    Telefone = from.Telefone,
                    Status = from.Status.ToString(),
                };
            }

            return null;
        }
    }
}
