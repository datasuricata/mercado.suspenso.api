using mercadosuspenso.domain.Models;
using System.Collections.Generic;

namespace mercadosuspenso.domain.Interfaces.Providers
{
    public interface IAuthenticationProvider
    {
        string CreateAccessToken(Usuario usuario, List<string> roles = null);
    }
}