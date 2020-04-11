using mercadosuspenso.domain.Enums;
using mercadosuspenso.domain.Interfaces.Services;
using mercadosuspenso.domain.Models;
using mercadosuspenso.orm.Repository;
using System.Threading.Tasks;

namespace mercadosuspenso.service.Services
{
    public class ParticipanteService : IParticipanteService
    {
        private readonly IRepository<Participante> repository;

        public ParticipanteService(IRepository<Participante> repository)
        {
            this.repository = repository;
        }

        public async Task AdicionarAsync(string nome, string cpf, string rg, string telefone)
        {
            var participante = new Participante(nome, cpf, rg, telefone);

            var registrado = await repository.ByAsync(p => p.Ativo && p.Cpf == cpf);

            if (registrado != null)
            {
                var recusado = registrado.Status == RegistroStatus.Refused;

                if (recusado)
                {

                    //todo notification
                }

                //todo notification
            }

            //todo bater no CadUnico se for valido
            //http://www.transparencia.gov.br/api-de-dados/bolsa-familia-disponivel-por-cpf-ou-nis?codigo=09319765960&anoMesReferencia=202001&anoMesCompetencia=202001&pagina=1

            await repository.InsertAsync(participante);
        }
    }
}

public class TitularBolsaFamilia
{
    public string CpfFormatado { get; set; }
    public bool MultiploCadastro { get; set; }
    public string Nis { get; set; }
    public string Nome { get; set; }
}

public class BolsaFamilia
{
    public string DataMesCompetencia { get; set; }
    public string DataMesReferencia { get; set; }
    public int Id { get; set; }
    public int QuantidadeDependentes { get; set; }
    public TitularBolsaFamilia TitularBolsaFamilia { get; set; }
    public int Valor { get; set; }
}