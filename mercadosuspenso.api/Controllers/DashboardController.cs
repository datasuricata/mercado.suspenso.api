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
    [Route("dashboard")]
    [Authorize(Policy = "reports")]
    public class DashboardController : ControllerBase
    {
        private readonly IDistribuidorService distribuidorService;
        private readonly IVarejistaService varejistaService;
        private readonly IEstoqueService estoqueService;

        public DashboardController(IEstoqueService estoqueService, IDistribuidorService distribuidorService, IVarejistaService varejistaService)
        {
            this.estoqueService = estoqueService;
            this.distribuidorService = distribuidorService;
            this.varejistaService = varejistaService;
        }

        [HttpGet("total/distribuidor")]
        [SwaggerOperation(Summary = "Total de distribuidores", Description = "Total distribuidores cadastrado na base agrupado por status de cadastro")]
        [SwaggerResponse(200, "Sucesso", type: typeof(IEnumerable<ContadorDto>))]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> TotalDistribuidor()
        {
            var dto = await distribuidorService.TotalAsync();

            return Ok(dto);
        }

        [HttpGet("total/varejista")]
        [SwaggerOperation(Summary = "Total de varejista", Description = "Total varejistas cadastrados na base agrupados por status de cadastro")]
        [SwaggerResponse(200, "Sucesso", type: typeof(IEnumerable<ContadorDto>))]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> TotalVarejista()
        {
            var dto = await varejistaService.TotalAsync();

            return Ok(dto);
        }

        [HttpGet("total/estoque")]
        [SwaggerOperation(Summary = "Total do estoque", Description = "Total estoque cadastrados na base agrupado por status de vistoria")]
        [SwaggerResponse(200, "Sucesso", type: typeof(IEnumerable<ContadorDto>))]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> TotalEstoque()
        {
            var dto = await estoqueService.TotalAsync();

            return Ok(dto);
        }
    }
}