using mercadosuspenso.domain.Enums;
using mercadosuspenso.domain.Exceptions;
using mercadosuspenso.domain.Extensions;
using mercadosuspenso.domain.Interfaces.Services;
using mercadosuspenso.domain.Models;
using mercadosuspenso.domain.Models.External;
using mercadosuspenso.orm.Repository;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace mercadosuspenso.service.Services
{
    public class ParticipanteService : IParticipanteService
    {
        private delegate void Assert(bool error, string message);

        private readonly IRepository<Participante> repository;

        public ParticipanteService(IRepository<Participante> repository)
        {

            this.repository = repository;
        }

        public async Task AdicionarAsync(string nome, string cpf, string rg, string telefone)
        {
            var participante = new Participante(nome, cpf, rg, telefone);

            var registrado = await repository.ByAsync(p => p.Ativo && p.Cpf == participante.Cpf);

            if (registrado != null)
            {
                Assert Quando = Domain.Validate;

                Quando(registrado.Status == RegistroStatus.Refused, "Existem algum problema com seu cadastro, contate o suporte");

                Quando(registrado != null, "Já um cadastro processado ou pendente com estes dados");
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
        }

        public async Task AprovarRecusarAsync(string cpf)
        {
            var participante = await repository.ByAsync(p => p.Ativo && p.Cpf == cpf.CleanFormat(), false);

            if (participante.Status == RegistroStatus.Aproved)
            {
                participante.Recusar();
            }
            if (participante.Status == RegistroStatus.Pendent || participante.Status == RegistroStatus.Refused)
            {
                participante.Aprovar();
            }
        }

        private async Task<bool> VerificaCadastroPortalTransparenciaAsync(string cpf)
        {
            var date = DateTime.Now.ToString("yyyyMM");

            var uri = new UriBuilder("http://www.transparencia.gov.br/")
            {
                Path = $"http://www.transparencia.gov.br/api-de-dados/bolsa-familia-disponivel-por-cpf-ou-nis?codigo={cpf}&anoMesReferencia={date}&anoMesCompetencia={date}&pagina=1",
            };

            using var client = new HttpClient();

            var response = await client.GetAsync(uri.ToString());

            if (response.IsSuccessStatusCode)
            {
                var obj = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<BolsaFamilia>(obj);

                return result != null;
            }
            else
                return false;
        }
    }
}