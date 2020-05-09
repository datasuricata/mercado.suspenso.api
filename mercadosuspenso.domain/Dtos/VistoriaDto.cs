using mercadosuspenso.domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace mercadosuspenso.domain.Dtos
{
    public class VistoriaDto
    {
        public string Id { get; set; }
        public DoacaoDto Doacao { get; set; }

        public static VistoriaDto From(Vistoria from)
        {
            if (from != null)
            {
                return new VistoriaDto
                {
                    Id = from.Id,
                    Doacao = DoacaoDto.From(from.Doacao),
                };
            }

            return null;
        }
    }
}
