using HtmlAgilityPack;
using mercadosuspenso.domain.Dtos;
using mercadosuspenso.domain.Enums;
using mercadosuspenso.domain.Exceptions;
using mercadosuspenso.domain.Extensions;
using mercadosuspenso.domain.Interfaces.Services;
using mercadosuspenso.domain.Models;
using mercadosuspenso.domain.Security;
using mercadosuspenso.domain.Settings;
using mercadosuspenso.orm;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mercadosuspenso.service.Services
{
    public class EstoqueService : IEstoqueService
    {
        private delegate void Assert(bool error, string message);
        private Assert Validar = DomainException.Validate;

        private readonly ISmtpService smtp;
        private readonly NFCeSettings settings;
        private readonly MercadoDbContext context;
        private readonly ClaimsPrincipal me;

        public EstoqueService(ISmtpService smtp, IPrincipal principal, MercadoDbContext context, IOptions<NFCeSettings> options)
        {
            this.smtp = smtp;
            this.context = context;
            this.me = (ClaimsPrincipal)principal;
            this.settings = options.Value;
        }

        public async Task<ProcessamentoDto> EntradaAsync(string chave, int versao, int ambiente, int identificador, string hash)
        {
            var varejistaId = me.FindFirst(ClaimsConstant.Id).Value;

            bool sucesso = true;
            string mensagem = default;
            var doacaoProdutos = new List<DoacaoProduto>();

            using var transaction = context.Database.BeginTransaction();

            try
            {
                var varejista = await context.Varejista
                    .Include(i => i.Endereco)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(varejista => varejista.Ativo && varejista.Id == varejistaId);

                Validar(varejista == null,
                    "Loja inativa contate o suporte");

                Validar(varejista.Status != RegistroStatus.Aprovado,
                    "Loja pendente de ativação, contate o suporte");

                var recipentes = await context.Vinculo
                    .Include(i => i.Distribuidor)
                    .Include(i => i.Distribuidor.Usuario)
                    .Where(vinculo => vinculo.Ativo && vinculo.VarejistaId == varejistaId && vinculo.Distribuidor.Status == RegistroStatus.Aprovado)
                    .AsNoTracking().ToListAsync();

                Validar(recipentes == null,
                    "Não existe nenhum distribuidor aprovado vinculado a sua loja, contate o suporte");

                var produtos = await CarregarNotaFiscalAsync(chave, versao, ambiente, identificador, hash);

                Validar(produtos == null,
                    "Não foi possível ler o qr code, verifique com o suporte se o problema persistir");

                Validar(!produtos.Any(),
                    "Deve ter ao menos um produto para doação, verifique os dados e tente novamente");

                await context.Produto.AddRangeAsync(produtos);

                var obj = new { Chave = chave, Versao = versao, Ambiente = ambiente, Identificador = identificador, HashCode = hash };
                var doacao = new Doacao(null, JsonConvert.SerializeObject(obj), varejistaId);
                await context.Doacao.AddAsync(doacao);

                var vistoria = new Vistoria(doacao);
                await context.Vistoria.AddAsync(vistoria);

                foreach (var produto in produtos)
                {
                    doacaoProdutos.Add(new DoacaoProduto(doacao, produto));
                }

                await context.DoacaoProduto.AddRangeAsync(doacaoProdutos);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                foreach(var r in recipentes)
                {
                    var email = r.Distribuidor.Usuario.Email;

                    await smtp.EnviarAsync(smtp.ResgateDoacao(email, varejista.ToString(), vistoria.Hash));
                }
            }
            catch (Exception)
            {
                sucesso = false;
                mensagem = "Produto não integrado, contate o suporte";

                await transaction.RollbackAsync();
            }

            return new ProcessamentoDto
            {
                Mensagem = mensagem,
                Sucesso = sucesso,
            };
        }

        public async Task ResgatarAsync(string rastreio, string cnpj)
        {
            //todo passar para a camada de serviço correta
            var vistoria = await context.Vistoria.FirstOrDefaultAsync
            (
                vistoria =>
                vistoria.Ativo &&
                vistoria.Hash.Contains(rastreio, StringComparison.InvariantCultureIgnoreCase)
            );

            Validar(vistoria == null,
                "Sem registros encontrados");

            Validar(vistoria.Status != VistoriaStatus.Entrada,
                "Produtos já receberam a retirada do estoque");

            //todo passar para a camada de serviço correta
            var distribuidor = await context.Distribuidor.AsNoTracking().FirstOrDefaultAsync
            (
                distribuidor =>
                distribuidor.Ativo &&
                distribuidor.Cnpj == cnpj.CleanFormat()
            );

            Validar(distribuidor.Status == RegistroStatus.Pendente,
                "Distribuidor com cadastro pendente de aprovação pelos moderadores");

            Validar(distribuidor.Status == RegistroStatus.Recusado,
                "Distribuidor com cadastro recusado pelos moderadores");

            vistoria.Resgatar(distribuidor.Id);

            context.Update(vistoria);
            await context.SaveChangesAsync();
        }

        public async Task RetirarAsync(string rastreio, string cpf)
        {
            //todo passar para a camada de serviço correta
            var vistoria = await context.Vistoria.FirstOrDefaultAsync
            (
                vistoria =>
                vistoria.Ativo &&
                vistoria.Hash.Contains(rastreio, StringComparison.InvariantCultureIgnoreCase)
            );

            Validar(vistoria == null,
                "Sem registros encontrados");

            Validar(vistoria.Status == VistoriaStatus.Entrada,
                "Produtos pendentes de retirada no varejista");

            Validar(vistoria.Status == VistoriaStatus.Retirada,
                "Produtos já receberam a retirada do estoque");

            //todo passar para a camada de serviço correta
            var participante = await context.Participante.AsNoTracking().FirstOrDefaultAsync
            (
                participante =>
                participante.Ativo &&
                participante.Cpf == cpf.CleanFormat()
            );

            Validar(participante.Status == RegistroStatus.Pendente,
                "Participante com cadastro pendente de aprovação pelos moderadores");

            Validar(participante.Status == RegistroStatus.Recusado,
                "Participante com cadastro recusado pelos moderadores");

            vistoria.Retirar(participante.Id);

            context.Update(vistoria);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ContadorDto>> TotalAsync()
        {
            return await context.Vistoria.AsQueryable().GroupBy(a => a.Status).Select(a => new ContadorDto
            {
                Titulo = $"Doações com status {a.Key.ToString().ToLower()}",
                Status = a.Key.ToString(),
                Total = a.Count(/*s => s.Status == a.Key*/),
            }).ToListAsync();
        }

        public async Task<IEnumerable<DoacaoDto>> EstoquePorVarejistaIdAsync(string id)
        {
            var doacoes = await context.Doacao.Include(a => a.DoacaoProdutos).ThenInclude(a => a.Produto)
                .Where(w => w.VarejistaId == id).AsNoTracking().ToListAsync();

            return doacoes.Select(DoacaoDto.From);
        }

        public async Task<IEnumerable<DoacaoDto>> EstoquePorDistribuidorIdAsync(string id)
        {
            var doacoes = await context.Vistoria.Include(a => a.Doacao).ThenInclude(a => a.DoacaoProdutos)
                .Where(w => w.DistribudidorId == id).AsNoTracking().ToListAsync();

            return doacoes.Select(a => DoacaoDto.From(a.Doacao));
        }

        public async Task<IEnumerable<VistoriaDto>> EstoqueLogadoAsync()
        {
            var id = me.FindFirst(ClaimsConstant.Id).Value;

            var profileId = int.Parse(me.FindFirst(ClaimsConstant.ProfileId)?.Value);

            var query = context.Vistoria.Include(a => a.Doacao).ThenInclude(at => at.DoacaoProdutos).AsQueryable();

            return (UsuarioTipo)profileId switch
            {
                UsuarioTipo.Varejo => (await query.Where
                (
                    vistoria =>
                    vistoria.Doacao.VarejistaId == id
                ).AsNoTracking().ToListAsync()).Select(VistoriaDto.From),

                UsuarioTipo.Distribuidor => (await query.Where
                (
                    vistoria =>
                    vistoria.DistribudidorId == id
                ).AsNoTracking().ToListAsync()).Select(VistoriaDto.From),

                UsuarioTipo.Administrador => (await query.AsNoTracking().ToListAsync()).Select(VistoriaDto.From),
                _ => null,
            };
        }

        private async Task<List<Produto>> CarregarNotaFiscalAsync(string chave, int versao, int ambiente, int identificador, string hash)
        {
            try
            {
                var produtos = new List<Produto>();
                
                using var client = new HttpClient
                {
                    BaseAddress = new Uri("http://www.fazenda.pr.gov.br/")
                };

                var path = $"nfce/qrcode/?p={chave}|{versao}|{ambiente}|{identificador}|{hash}";
                var response = await client.GetAsync(path);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var obj = await response.Content.ReadAsStringAsync();
                    var doc = new HtmlDocument();

                    doc.LoadHtml(obj);

                    var itensNode = doc.DocumentNode.SelectNodes(settings.NodeColum)
                        .Where(x => !x.HasClass(settings.NodeAcceptedCriteria));

                    foreach (var node in itensNode)
                    {
                        var nome = node.Descendants()
                            .Where(n => n.HasClass(settings.ProductName)).FirstOrDefault()?.InnerText;

                        var code = node.Descendants()
                            .Where(n => n.HasClass(settings.ProductCode)).FirstOrDefault()?.InnerText;

                        var qntd = node.Descendants()
                            .Where(n => n.HasClass(settings.ProductQntd)).FirstOrDefault().GetDirectInnerText();

                        produtos.Add(new Produto(decimal.Parse(qntd), nome, Regex.Replace(code, "[^0-9]", "")));
                    }

                    return produtos;
                }

                return produtos;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}