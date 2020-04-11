using mercadosuspenso.api.Commands;
using mercadosuspenso.domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace mercadosuspenso.api.Controllers
{
    [ApiController]
    [Route("participante")]
    public class ParticipanteController : ControllerBase
    {
        private readonly IParticipanteService service;

        public ParticipanteController(IParticipanteService service)
        {
            this.service = service;
        }

        [AllowAnonymous]
        [HttpPost]
        [SwaggerOperation(Summary = "Novo cadastro no portal", Description = "Novo cadastro de participante integrado com a api de transparencia governamental")]
        [SwaggerResponse(200, "Participante cadastrado")]
        [SwaggerResponse(400, "Validações de registro")]
        public async Task<IActionResult> Post([FromBody] ParticipanteCommand command)
        {
            await service.AdicionarAsync(command.Nome, command.Cpf, command.Rg, command.Telefone);

            var json = new { Status = 200, Message = "Participante cadastrado" };

            return Ok(json);
        }
    }
}