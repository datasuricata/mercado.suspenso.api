using mercadosuspenso.domain.Dtos;
using mercadosuspenso.domain.Enums;
using mercadosuspenso.domain.Exceptions;
using mercadosuspenso.domain.Extensions;
using mercadosuspenso.domain.Interfaces.Services;
using mercadosuspenso.domain.Models;
using mercadosuspenso.domain.Models.External;
using mercadosuspenso.orm.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace mercadosuspenso.service.Services
{
    public class ParticipanteService : IParticipanteService
    {
        private delegate void Assert(bool error, string message);
        private Assert Validar = DomainException.Validate;

        private readonly ISmtpService smtp;
        private readonly IRepository<Participante> repository;

        public ParticipanteService(ISmtpService smtp, IRepository<Participante> repository)
        {
            this.smtp = smtp;
            this.repository = repository;
        }

        public async Task AdicionarAsync(string nome, string cpf, string rg, string telefone, string email)
        {
            var participante = new Participante(nome, cpf, rg, telefone, email);

            var registrado = await repository.ByAsync(p => p.Ativo && p.Cpf == participante.Cpf);

            if (registrado != null)
            {
                Validar(registrado.Status == RegistroStatus.Recusado,
                    "Existem algum problema com seu cadastro, contate o suporte");

                Validar(registrado != null,
                    "Já um cadastro processado ou pendente com estes dados");
            }
            else
            {
                var existente = await VerificaCadastroPortalTransparenciaAsync(participante.Cpf);

                if (existente)
                {
                    participante.Aprovar();
                }
            }

            await repository.InsertAsync(participante);

            if (!string.IsNullOrEmpty(email))
            {
                await smtp.EnviarAsync(smtp.BoasVindas(email, nome));
            }
        }

        public async Task AprovarRecusarAsync(string id)
        {
            var participante = await repository.ByAsync(p => p.Ativo && p.Id == id, false);

            if (participante.Status == RegistroStatus.Aprovado)
            {
                participante.Recusar();
            }
            if (participante.Status == RegistroStatus.Pendente || participante.Status == RegistroStatus.Recusado)
            {
                participante.Aprovar();
            }

            await smtp.EnviarAsync(smtp.AprovaReprova(participante.Email, participante.Status));
        }

        public async Task<IEnumerable<ContadorDto>> TotalAsync()
        {
            return await repository.Queryable(true).GroupBy(x => x.Status).Select(a => new ContadorDto
            {
                Titulo = $"Participantes com status {a.Key.ToString().ToLower()}",
                Status = a.Key.ToString(),
                Total = a.Count(/*s => s.Status == a.Key*/),
            }).ToListAsync();
        }

        public async Task<IEnumerable<EntidadeDto>> ListarAsync()
        {
            var entidades = await repository.ListAsync();

            return entidades.OrderBy(a => a.CriadoEm).Select(EntidadeDto.From);
        }

        public async Task<EntidadeDto> PorIdAsync(string id)
        {
            var entidade = await repository.ByIdAsync(id);

            return EntidadeDto.From(entidade);
        }

        private async Task<bool> VerificaCadastroPortalTransparenciaAsync(string cpf)
        {
            var date = DateTime.Now.AddMonths(-1).ToString("yyyyMM");

            using var client = new HttpClient
            {
                BaseAddress = new Uri("http://www.transparencia.gov.br/")
            };

            var path = $"http://www.transparencia.gov.br/api-de-dados/bolsa-familia-disponivel-por-cpf-ou-nis?codigo={cpf}&anoMesReferencia={date}&anoMesCompetencia={date}&pagina=1";

            var response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                var obj = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<BolsaFamilia>>(obj);

                return result.Count > 0;
            }
            else
                return false;
        }
    }
}