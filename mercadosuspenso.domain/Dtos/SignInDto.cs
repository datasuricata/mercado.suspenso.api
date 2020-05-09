using System.Collections.Generic;

namespace mercadosuspenso.domain.Dtos
{
    public class SignInDto
    {
        public string AccessToken { get; set; }
        public string Type { get; set; }
        public List<string> Roles { get; set; }
    }
}
