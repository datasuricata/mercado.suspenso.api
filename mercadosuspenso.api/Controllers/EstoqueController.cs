using HtmlAgilityPack;
using mercadosuspenso.api.Commands;
using mercadosuspenso.domain.Dtos;
using mercadosuspenso.domain.Interfaces.Services;
using mercadosuspenso.domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mercadosuspenso.api.Controllers
{
    [ApiController]
    [Route("estoque")]
    [Authorize(Policy = "stockist")]
    public class EstoqueController : ControllerBase
    {
        private readonly IEstoqueService service;

        public EstoqueController(IEstoqueService service)
        {
            this.service = service;
        }

        [AllowAnonymous]
        [HttpPost("test-qrcode")]
        [SwaggerOperation(Summary = "Teste integração", Description = "Lista os produtos direto da secretaria da fazenda")]
        public async Task<IActionResult> GetQr([FromBody]EntradaCommand command)
        {
            using var client = new HttpClient
            {
                BaseAddress = new Uri("http://www.fazenda.pr.gov.br/")
            };

            var path = $"nfce/qrcode/?p={command.Chave}|{command.Versao}|{command.Ambiente}|{command.Identificador}|{command.Hash}";

            var response = await client.GetAsync(path);

            var obj = await response.Content.ReadAsStringAsync();

            var doc = new HtmlDocument();

            doc.LoadHtml(obj);

            var produtos = new List<ProdutoDto>();

            var itensNodes = doc.DocumentNode.SelectNodes("//tr/td").Where(x => !x.HasClass("txtTit3"));

            foreach (var node in itensNodes)
            {
                var nome = node.Descendants().Where(n => n.HasClass("txtTit2")).FirstOrDefault()?.InnerText;
                var codigo = node.Descendants().Where(n => n.HasClass("RCod")).FirstOrDefault()?.InnerText;
                var qntd = node.Descendants().Where(n => n.HasClass("Rqtd")).FirstOrDefault().GetDirectInnerText();

                var produto = new ProdutoDto
                {
                    Id = Guid.NewGuid().ToString(),
                    Codigo = Regex.Replace(codigo, "[^0-9]", ""),
                    Nome = nome,
                    Quantidade = decimal.Parse(qntd)
                };

                produtos.Add(produto);
            }

            return Ok(produtos);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Listar o estoque", Description = "Lista os produtos agrupados por vistorias de acordo com o perfil logado")]
        [SwaggerResponse(200, "Sucesso", type: typeof(IEnumerable<VistoriaDto>))]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> Listar()
        {
            var dto = await service.EstoqueLogadoAsync();

            return Ok(dto);
        }

        [HttpPost("entrada")]
        [SwaggerOperation(Summary = "Nova entrada no estoque", Description = "Cadastra uma nova doação e vincula os produtos com a secretaria da fazenda no estoque pelo qrcode")]
        [SwaggerResponse(200, "Sucesso", type: typeof(ProcessamentoDto))]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> Entrada([FromBody] EntradaCommand command)
        {
            var dto = await service.EntradaAsync(command.Chave, command.Versao, command.Ambiente, command.Identificador, command.Hash);

            return Ok(dto);
        }

        [HttpPatch("resgate")]
        [SwaggerOperation(Summary = "Resgate no estoque", Description = "Resgate de produtos pelo distribuidor no varejista, deve informar o hash que recebeu por e-mail e o cnpj para retirada")]
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> Resgate([FromBody] RetiradaCommand command)
        {
            await service.RetirarAsync(command.Hash, command.Identificador);

            return Ok();
        }

        [HttpPatch("retirada")]
        [SwaggerOperation(Summary = "Retirada no estoque", Description = "Retirada de produtos pelo participante no distribuidor, deve informar o hash que recebeu por e-mail e o cpf para retirada")]
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(400, "Dados inválidos", type: typeof(ProblemDto))]
        [SwaggerResponse(403, "Não permitido", type: typeof(ProblemDto))]
        public async Task<IActionResult> Retirada([FromBody] RetiradaCommand command)
        {
            await service.RetirarAsync(command.Hash, command.Identificador);

            return Ok();
        }
    }
}