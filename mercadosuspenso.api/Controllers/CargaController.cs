using mercadosuspenso.domain.Dtos;
using mercadosuspenso.service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace mercadosuspenso.api.Controllers
{
    [ApiController]
    [Route("carga")]
    [Authorize(Policy = "import")]
    public class CargaController : ControllerBase
    {
        private readonly ICargaService service;

        public CargaController(ICargaService service)
        {
            this.service = service;
        }

        [HttpPost("participante")]
        [SwaggerOperation(Summary = "Importar particpantes", Description = "Importa um arquivo excel contendo participantes")]
        [SwaggerResponse(200, "Sucesso", type: typeof(ProcessamentoDto))]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> CargaParticipante(IFormFile file)
        {
            var dto = await service.ImportarParticipantes(file);

            return Ok(dto);
        }
    }
}
