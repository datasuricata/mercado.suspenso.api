using mercadosuspenso.domain.Enums;
using System.Collections.Generic;

namespace mercadosuspenso.api.Commands
{
    public class VincularCommand
    {
        public string ParceiroId { get; set; }
        public List<string> ParceirosIds { get; set; }
    }
}