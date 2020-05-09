using mercadosuspenso.api.Commands;
using mercadosuspenso.domain.Dtos;
using mercadosuspenso.domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mercadosuspenso.api.Controllers
{
    [ApiController]
    [Route("oauth")]
    public class AuthController : ControllerBase
    {
        private readonly IAutenticacaoService service;
        public AuthController(IAutenticacaoService service)
        {
            this.service = service;
        }

        [AllowAnonymous]
        [HttpPost("signin/varejista")]
        [SwaggerOperation(Summary = "Login e autorização da Loja", Description = "Use este endpoint para realizar o login do usuário varejista")]
        [SwaggerResponse(200, "Sucesso", type: typeof(SignInDto))]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        public async Task<IActionResult> SignInVarejista([FromBody] SigninCommand command)
        {
            var dto = await service.SignInVarejistaAsync(command.Username, command.Password);

            return Ok(dto);
        }

        [AllowAnonymous]
        [HttpPost("signin/distribuidor")]
        [SwaggerOperation(Summary = "Login e autorização do distribuidor", Description = "Use este endpoint para realizar o login do usuário distribuidor")]
        [SwaggerResponse(200, "Sucesso", type: typeof(SignInDto))]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        public async Task<IActionResult> SignInDistribuidor([FromBody] SigninCommand command)
        {
            var dto = await service.SignInDistribuidorAsync(command.Username, command.Password);

            return Ok(dto);
        }

        [AllowAnonymous]
        [HttpPost("signin/admin")]
        [SwaggerOperation(Summary = "Login e autorização do admin", Description = "Use este endpoint para realizar o login do usuário administrador")]
        [SwaggerResponse(200, "Sucesso", type: typeof(SignInDto))]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        public async Task<IActionResult> SignInAdmin([FromBody] SigninCommand command)
        {
            var dto = await service.SignInAdminAsync(command.Username, command.Password);

            return Ok(dto);
        }
    }
}