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
        private readonly IRepository<Participante> participanteRepository;

        public ParticipanteService(ISmtpService smtp, IRepository<Participante> participanteRepository)
        {
            this.smtp = smtp;
            this.participanteRepository = participanteRepository;
        }

        public async Task AdicionarAsync(string nome, string cpf, string rg, string telefone, string email, string cep, string logradouro, string numero, string complemento, string bairro, string cidade, string estado)
        {
            var participante = new Participante(nome, cpf, rg, telefone, email)
            {
                Endereco = new Endereco(cep, logradouro, numero, complemento, bairro, cidade, estado)
            };

            var registrado = await participanteRepository.PorAsync(p => p.Ativo && p.Cpf == participante.Cpf);

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

            await participanteRepository.InserirAsync(participante);

            if (!string.IsNullOrEmpty(email))
            {
                await smtp.EnviarAsync(smtp.BoasVindas(email, nome));
            }
        }

        public async Task AprovarRecusarAsync(string id)
        {
            var participante = await participanteRepository.PorAsync(p => p.Ativo && p.Id == id, noTracking: false);

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
            return await participanteRepository.Queryable().GroupBy(x => x.Status).Select(a => new ContadorDto
            {
                Titulo = $"Participantes com status {a.Key.ToString().ToLower()}",
                Status = a.Key.ToString(),
                Total = a.Count(/*s => s.Status == a.Key*/),
            }).ToListAsync();
        }

        public async Task<IEnumerable<EntidadeDto>> ListarPorStatusAsync(RegistroStatus status)
        {
            return (await participanteRepository.ListarAsync(noTracking: false, c => c.Status == status))
                .OrderBy(a => a.CriadoEm).Select(EntidadeDto.From);
        }

        public async Task<EntidadeDto> PorIdAsync(string id)
        {
            return EntidadeDto.From(await participanteRepository.PorIdAsync(id));
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