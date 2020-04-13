using mercadosuspenso.domain.Enums;
using mercadosuspenso.domain.Exceptions;
using mercadosuspenso.domain.Extensions;
using mercadosuspenso.domain.Interfaces.Services;
using mercadosuspenso.domain.Models;
using mercadosuspenso.orm.Repository;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace mercadosuspenso.service.Services
{
    public class DistribuidorService : IDistribuidorService
    {
        private delegate void Assert(bool error, string message);

        private readonly IRepository<Distribuidor> repository;
        private readonly ClaimsPrincipal logged;

        public DistribuidorService(IPrincipal principal, IRepository<Distribuidor> repository)
        {
            this.repository = repository;
            this.logged = (ClaimsPrincipal)principal;
        }

        public async Task AdicionarAsync(string razaoSocial, string nome, string cnpj, string telefone, string email, string senha)
        {
            var distribuidor = new Distribuidor(razaoSocial, nome, cnpj, telefone)
            {
                Usuario = new Usuario(email, senha),
            };

            var registrado = await repository.ByAsync(p => p.Ativo && p.Cnpj == distribuidor.Cnpj);

            if (registrado != null)
            {
                Assert Quando = Domain.Validate;

                Quando(registrado.Status == RegistroStatus.Refused, "Existem algum problema com seu cadastro, contate o suporte");

                Quando(registrado != null, "Já um cadastro processado ou pendente com estes dados");
            }

            await repository.InsertAsync(distribuidor);
        }

        public async Task AprovarRecusarAsync(string cnpj)
        {
            var distribuidor = await repository.ByAsync(p => p.Ativo && p.Cnpj == cnpj.CleanFormat(), false);

            if (distribuidor.Status == RegistroStatus.Aproved)
            {
                distribuidor.Recusar();
            }
            if (distribuidor.Status == RegistroStatus.Pendent || distribuidor.Status == RegistroStatus.Refused)
            {
                distribuidor.Aprovar();
            }
        }

        public async Task AdicionarOuAlterarEnderecoLogadoAsync(string cep, string logradouro, string numero, string complemento, string bairro, string cidade, string estado)
        {
            //var id = logged.FindFirst(ClaimsConstant.Id).Value;
            var id = default(string);

            var distribuidor = await repository.ByAsync(c => c.UsuarioId == id, false);

            if (distribuidor.Endereco is null)
            {
                distribuidor.Endereco = new Endereco(cep, logradouro, numero, complemento, bairro, cidade, estado);
            }
            else
            {
                distribuidor.Endereco.Cep = cep;
                distribuidor.Endereco.Logradouro = logradouro;
                distribuidor.Endereco.Numero = numero;
                distribuidor.Endereco.Complemento = complemento;
                distribuidor.Endereco.Bairro = bairro;
                distribuidor.Endereco.Cidade = cidade;
                distribuidor.Endereco.Estado = estado;

                distribuidor.Endereco.Validar();
            }

            await repository.UpdateAsync(distribuidor);
        }
    }
}