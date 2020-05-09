using mercadosuspenso.domain.Enums;
using mercadosuspenso.domain.Interfaces.Providers;
using mercadosuspenso.domain.Models;
using mercadosuspenso.domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace mercadosuspenso.api.Providers
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly IConfiguration config;

        public AuthenticationProvider(IConfiguration config)
        {
            this.config = config;
        }

        public string CreateAccessToken(Usuario usuario, List<string> roles = null)
        {
            var jwt = new JwtSecurityTokenHandler();

            var bytes = Encoding.ASCII.GetBytes(config["SecurityKey"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimsConstant.Id, usuario.Id),
                new Claim(ClaimsConstant.Username, usuario.Email),
                new Claim(ClaimsConstant.ProfileId, ((int)usuario.Tipo).ToString()),
                new Claim(ClaimsConstant.Profile, usuario.Tipo.ToString())
            };

            if (roles != null)
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var key = new SymmetricSecurityKey(bytes);

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var payload = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = credential,
                IssuedAt = DateTime.UtcNow,
            };

            var security = jwt.CreateToken(payload);

            return jwt.WriteToken(security);
        }
    }
}
