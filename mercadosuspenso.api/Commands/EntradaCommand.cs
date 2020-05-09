using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercadosuspenso.api.Commands
{
    public class EntradaCommand
    {
        public string Chave { get; set; }
        public int Versao { get; set; }
        public int Ambiente { get; set; }
        public int Identificador { get; set; }
        public string Hash { get; set; }
    }
}
