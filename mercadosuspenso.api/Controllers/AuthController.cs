using mercadosuspenso.api.Commands;
using mercadosuspenso.domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace mercadosuspenso.api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration config;

        public AuthController(IConfiguration config)
        {
            this.config = config;
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        [SwaggerOperation(Summary = "SignIn de autorização", Description = "Use este endpoint para realizar o login do usuário")]
        [SwaggerResponse(200, "Token é valido")]
        public IActionResult SignIn([FromBody] SignInCommand command)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(config["SecurityKey"]);

            var payload = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, command.Email) }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = DateTime.UtcNow,
            };

            var json = new
            {
                Status = 200,
                command.Email,
                AccessToken = handler.WriteToken(handler.CreateToken(payload)),
            };

            return Ok(json);
        }

        [HttpGet("sample")]
        [SwaggerOperation(Summary = "Teste de autorização", Description = "Use este endpoint para realizar o funcionamento do token resgatado pelo signin")]
        [SwaggerResponse(200, "Token é valido")]
        [SwaggerResponse(401, "Não autorizado")]
        public IActionResult Sample()
        {
            var json = new { Status = 200, Message = "Token é valido" };

            return Ok(json);
        }
    }
}