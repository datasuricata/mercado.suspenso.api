using mercadosuspenso.api.Commands;
using mercadosuspenso.domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace mercadosuspenso.api.Controllers
{
    [ApiController]
    [Route("distribuidor")]
    public class DistribuidorController : ControllerBase
    {
        private readonly IDistribuidorService service;

        public DistribuidorController(IDistribuidorService service)
        {
            this.service = service;
        }

        [AllowAnonymous]
        [HttpPost]
        [SwaggerOperation(Summary = "Novo cadastro de distribuidor", Description = "Novo cadastro de distribuidor pendente de aprovação")]
        [SwaggerResponse(200, "Distribuidor cadastrado")]
        [SwaggerResponse(400, "Validações de negócio")]
        public async Task<IActionResult> Post([FromBody] DistribuidorCommand command)
        {
            await service.AdicionarAsync(command.RazaoSocial, command.Representante, command.Cnpj, command.Telefone, command.Email, command.Senha);

            var json = new { Status = 200, Message = "Distribuidor cadastrado" };

            return Ok(json);
        }

        [HttpPost("aprovar-recusar")]
        [SwaggerOperation(Summary = "Aprovar ou recusar distribuidor", Description = "Aprova ou recusa o cadastro de um distribuidor na base de dados")]
        [SwaggerResponse(200, "Distribuidor alterado")]
        [SwaggerResponse(400, "Validações de negócio")]
        public async Task<IActionResult> AprovarRecusar([FromBody] AprovarRecusarCommand command)
        {
            await service.AprovarRecusarAsync(command.Cpf);

            var json = new { Status = 200, Message = "Cadastro alterado" };

            return Ok(json);
        }

        [HttpPut("endereco")]
        [SwaggerOperation(Summary = "Inserir ou alterar endereço", Description = "Altera ou inclui o registro de um novo endereço do distribuidor")]
        [SwaggerResponse(200, "Distribuidor alterado")]
        [SwaggerResponse(400, "Validações de negócio")]
        public async Task<IActionResult> Endereco([FromBody] EnderecoCommand command)
        {
            await service.AdicionarOuAlterarEnderecoLogadoAsync(command.Cep, command.Logradouro, command.Numero, command.Complemento, command.Bairro, command.Cidade, command.Estado);

            var json = new { Status = 200, Message = "Cadastro alterado" };

            return Ok(json);
        }
    }
}