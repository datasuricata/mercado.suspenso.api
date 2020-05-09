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
    [Route("distribuidor")]
    [Authorize(Policy = "dispenser")]
    public class DistribuidorController : ControllerBase
    {
        private readonly IDistribuidorService service;
        private readonly IVinculoService vinculoService;

        public DistribuidorController(IDistribuidorService service, IVinculoService vinculoService)
        {
            this.service = service;
            this.vinculoService = vinculoService;
        }

        [AllowAnonymous]
        [HttpPost]
        [SwaggerOperation(Summary = "Novo cadastro de distribuidor", Description = "Novo cadastro de distribuidor pendente de aprovação")]
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        public async Task<IActionResult> Post([FromBody] ParceiroCommand command)
        {
            await service.AdicionarAsync
            (
                command.RazaoSocial,
                command.Representante,
                command.Cnpj,
                command.Telefone,
                command.Email,
                command.Senha
            );

            return Ok();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Meus dados", Description = "Dados gerais do perfil logado")]
        [SwaggerResponse(200, "Sucesso", type: typeof(EntidadeDto))]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> MeusDados()
        {
            var dto = await service.MeusDadosAsync();

            return Ok(dto);
        }

        [HttpPatch]
        [SwaggerOperation(Summary = "Atualiza dados do varejista", Description = "Atualiza dados do varejista no banco de dados")]
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> Patch([FromBody] ParceiroCommand command)
        {
            await service.AtualizarAsync
            (
                command.RazaoSocial,
                command.Representante,
                command.Cnpj,
                command.Telefone
            );

            return Ok();
        }

        [HttpGet("vinculados")]
        [SwaggerOperation(Summary = "Parceiros Vinculados", Description = "Lista todos os parceiros vinculados a este estabelecimento")]
        [SwaggerResponse(200, "Sucesso", type: typeof(IEnumerable<VinculoDto>))]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> Vinculado()
        {
            var dto = await vinculoService.ListarPorUsuarioLoggadoAsync();

            return Ok(dto);
        }


        [HttpPut("endereco")]
        [SwaggerOperation(Summary = "Inserir ou alterar endereço", Description = "Altera ou inclui o registro de endereço do distribuidor")]
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> Endereco([FromBody] EnderecoCommand command)
        {
            await service.AdicionarOuAlterarEnderecoLogadoAsync
            (
                command.Cep,
                command.Logradouro,
                command.Numero,
                command.Complemento,
                command.Bairro,
                command.Cidade,
                command.Estado
            );

            return Ok();
        }
    }
}