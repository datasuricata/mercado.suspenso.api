﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercadosuspenso.api.Commands
{
    public class DistribuidorCommand
    {
        public string RazaoSocial { get; set; }
        public string Representante { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
