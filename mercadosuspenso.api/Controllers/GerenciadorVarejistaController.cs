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
    [Route("gerenciador/varejista")]
    [Authorize(Policy = "management")]
    public class GerenciadorVarejistaController : ControllerBase
    {
        private readonly IVarejistaService varejistaService;
        private readonly IEstoqueService estoqueService;
        private readonly IVinculoService vinculoService;

        public GerenciadorVarejistaController(IEstoqueService estoqueService, IVarejistaService varejistaService, IVinculoService vinculoService)
        {
            this.estoqueService = estoqueService;
            this.varejistaService = varejistaService;
            this.vinculoService = vinculoService;
        }

        [HttpGet("status")]
        [SwaggerOperation(Summary = "Listagem de varejista", Description = "Listagem de varejistas cadastrados na base por status (pendente = 0, ativo = 1, recusado = 2)")]
        [SwaggerResponse(200, "Sucesso", type: typeof(IEnumerable<EntidadeDto>))]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> Listar(RegistroStatus status)
        {
            var dto = await varejistaService.ListarPorStatusAsync(status);

            return Ok(dto);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retorna varejista", Description = "Retorna um varejista pelo identificador")]
        [SwaggerResponse(200, "Sucesso", type: typeof(EntidadeDto))]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> Detalhe(string id)
        {
            var dto = await varejistaService.PorIdAsync(id);

            return Ok(dto);
        }

        [HttpGet("vinculados/{id}")]
        [SwaggerOperation(Summary = "Retorna vinculados", Description = "Retorna parceiros vinculados ao varejista pelo identificador")]
        [SwaggerResponse(200, "Sucesso", type: typeof(IEnumerable<VinculoDto>))]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> Vinculados(string id)
        {
            var dto = await vinculoService.ListarPorVarejistaAsync(id);

            return Ok(dto);
        }

        [HttpPost("aprovar-recusar")]
        [SwaggerOperation(Summary = "Aprovar ou recusar varejista", Description = "Aprova ou recusa o cadastro de um varejista na base de dados pelo identificador")]
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> AprovarRecusar([FromBody] AuditarCommand command)
        {
            await varejistaService.AprovarRecusarAsync
            (
                command.Id
            );

            return Ok();
        }

        [HttpPost("vincular")]
        [SwaggerOperation(Summary = "Vincular parceiros", Description = "Vincula parceiros entre distribuidores e varejistas e vice-e-versa")]
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> Vincular([FromBody] VincularCommand command)
        {
            await vinculoService.VincularParceirosAsync(UsuarioTipo.Varejo, command.ParceiroId, command.ParceirosIds);

            return Ok();
        }

        [HttpPost("remover")]
        [SwaggerOperation(Summary = "Remover parceiros", Description = "Remover vinculo de parceiros entre distribuidores e varejistas e vice-e-versa")]
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> Remover([FromBody] VincularCommand command)
        {
            await vinculoService.RemoverVinculosAsync(command.ParceiroId, command.ParceirosIds);

            return Ok();
        }

        [HttpGet("estoque/{id}")]
        [SwaggerOperation(Summary = "Listar estoque por identificador", Description = "Lista estoque de acordo com o identificador do varejista")]
        [SwaggerResponse(200, "Sucesso", type: typeof(IEnumerable<DoacaoDto>))]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> Estoque(string id)
        {
            var dto = await estoqueService.EstoquePorVarejistaIdAsync(id);

            return Ok(dto);
        }
    }
}