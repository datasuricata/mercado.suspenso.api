using mercadosuspenso.api.Commands;
using mercadosuspenso.domain.Dtos;
using mercadosuspenso.domain.Enums;
using mercadosuspenso.domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mercadosuspenso.api.Controllers
{
    [ApiController]
    [Route("gerenciador/participante")]
    [Authorize(Policy = "management")]
    public class GerenciadorParticipanteController : ControllerBase
    {
        private readonly IParticipanteService participanteService;

        public GerenciadorParticipanteController(IParticipanteService participanteService)
        {
            this.participanteService = participanteService;
        }

      
        [HttpGet("status")]
        [SwaggerOperation(Summary = "Listagem de participantes", Description = "Listagem participantes cadastrado na base por status de registro")]
        [SwaggerResponse(200, "Sucesso", type: typeof(IEnumerable<EntidadeDto>))]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> ListarPorStatus(RegistroStatus status)
        {
            var dto = await participanteService.ListarPorStatusAsync(status);

            return Ok(dto);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retorna participante", Description = "Retorna um participante pelo identificador")]
        [SwaggerResponse(200, "Sucesso", type: typeof(EntidadeDto))]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> Detalhe(string id)
        {
            var dto = await participanteService.PorIdAsync(id);

            return Ok(dto);
        }

        [HttpPost("aprovar-recusar")]
        [SwaggerOperation(Summary = "Aprovar ou recusar participante", Description = "Aprova ou recusa o cadastro de um participante na base de dados pelo identificador")]
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> AprovarRecusar([FromBody] AuditarCommand command)
        {
            await participanteService.AprovarRecusarAsync
            (
                command.Id
            );

            return Ok();
        }
    }
}