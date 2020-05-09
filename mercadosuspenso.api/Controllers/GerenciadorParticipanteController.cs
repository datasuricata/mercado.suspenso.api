using mercadosuspenso.api.Commands;
using mercadosuspenso.domain.Dtos;
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

      
        [HttpGet]
        [SwaggerOperation(Summary = "Listagem de participantes", Description = "Listagem participantes cadastrado na base")]
        [SwaggerResponse(200, "Sucesso", type: typeof(IEnumerable<EntidadeDto>))]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> Listar()
        {
            var dto = await participanteService.ListAsync();

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