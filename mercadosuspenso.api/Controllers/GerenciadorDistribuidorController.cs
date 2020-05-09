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
    [Route("gerenciador/distribuidor")]
    [Authorize(Policy = "management")]
    public class GerenciadorDistribuidorController : ControllerBase
    {
        private readonly IDistribuidorService distribuidorService;
        private readonly IEstoqueService estoqueService;
        private readonly IVinculoService vinculoService;

        public GerenciadorDistribuidorController(IVinculoService vinculoService, IEstoqueService estoqueService, IDistribuidorService distribuidorService)
        {
            this.vinculoService = vinculoService;
            this.estoqueService = estoqueService;
            this.distribuidorService = distribuidorService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Listagem de distribuidores", Description = "Listagem distribuidores cadastrado na base")]
        [SwaggerResponse(200, "Sucesso", type: typeof(IEnumerable<EntidadeDto>))]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> Listar()
        {
            var dto = await distribuidorService.ListAsync();

            return Ok(dto);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retorna distribuidor", Description = "Retorna um distribuidor pelo identificador")]
        [SwaggerResponse(200, "Sucesso", type: typeof(EntidadeDto))]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> Detalhe(string id)
        {
            var dto = await distribuidorService.PorIdAsync(id);

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
        [SwaggerOperation(Summary = "Aprovar ou recusar distribuidor", Description = "Aprova ou recusa o cadastro de um distribuidor na base de dados pelo identificador")]
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> AprovarRecusar([FromBody] AuditarCommand command)
        {
            await distribuidorService.AprovarRecusarAsync
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
            await vinculoService.VincularParceirosAsync(UsuarioTipo.Distribuidor, command.ParceiroId, command.ParceirosIds);

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
        [SwaggerOperation(Summary = "Listar estoque por identificador", Description = "Lista estoque de acordo com o identificador do distribuidor")]
        [SwaggerResponse(200, "Sucesso", type: typeof(IEnumerable<DoacaoDto>))]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> Estoque(string id)
        {
            var dto = await estoqueService.EstoquePorDistribuidorIdAsync(id);

            return Ok(dto);
        }
    }
}