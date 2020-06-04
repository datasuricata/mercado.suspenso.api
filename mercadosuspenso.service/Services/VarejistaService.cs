using mercadosuspenso.domain.Dtos;
using mercadosuspenso.domain.Enums;
using mercadosuspenso.domain.Exceptions;
using mercadosuspenso.domain.Interfaces.Services;
using mercadosuspenso.domain.Models;
using mercadosuspenso.domain.Security;
using mercadosuspenso.orm.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace mercadosuspenso.service.Services
{
    public class VarejistaService : IVarejistaService
    {
        private delegate void Assert(bool error, string message);
        private Assert Validar = DomainException.Validate;

        private readonly ISmtpService smtp;
        private readonly IRepository<Varejista> varejistaRepository;
        private readonly IRepository<Aceite> aceiteRepository;

        private readonly ClaimsPrincipal me;

        public VarejistaService(ISmtpService smtp, IPrincipal principal, IRepository<Varejista> varejistaRepository)
        {
            this.smtp = smtp;
            this.varejistaRepository = varejistaRepository;
            this.me = (ClaimsPrincipal)principal;
        }

        public async Task AdicionarAsync(string razaoSocial, string representante, string cnpj, string telefone, string email, string senha, bool aceite)
        {
            var varejista = new Varejista(razaoSocial, representante, cnpj, telefone)
            {
                Usuario = new Usuario(email, senha),
            };

            varejista.Usuario.DefinirTipo(UsuarioTipo.Varejo);

            var registrado = await varejistaRepository.PorAsync(p => p.Ativo && p.Cnpj == varejista.Cnpj);

            if (registrado != null)
            {
                Validar(registrado.Status == RegistroStatus.Recusado,
                    "Existem algum problema com seu cadastro, contate o suporte");

                Validar(registrado != null,
                    "Já um cadastro processado ou pendente com estes dados");
            }

            Validar(aceite == false,
                "É preciso aceitar os termos para concluir o cadastro");

            await varejistaRepository.InserirAsync(varejista);

            var termo = new Aceite(varejista.UsuarioId, varejista.Cnpj);

            await aceiteRepository.InserirAsync(termo);

            await smtp.EnviarAsync(smtp.BoasVindas(email, representante));
        }

        public async Task AtualizarAsync(string razaoSocial, string representante, string cnpj, string telefone)
        {
            var id = me.FindFirst(ClaimsConstant.Id).Value;

            var varejista = await varejistaRepository.PorAsync(c => c.UsuarioId == id, noTracking: false);

            Validar(varejista == null,
                "Usuário não encontrado, talvez a sessão tenha expirado, tente logar novamente");

            varejista.RazaoSocial = razaoSocial;
            varejista.Representante = representante;
            varejista.Cnpj = cnpj;
            varejista.Telefone = telefone;

            await varejistaRepository.AtualizarAsync(varejista);
        }

        public async Task AprovarRecusarAsync(string id)
        {
            var varejista = await varejistaRepository.PorAsync(p => p.Ativo && p.Id == id, noTracking: false, i => i.Usuario);

            if (varejista.Status == RegistroStatus.Aprovado)
            {
                varejista.Recusar();
            }
            if (varejista.Status == RegistroStatus.Pendente || varejista.Status == RegistroStatus.Recusado)
            {
                varejista.Aprovar();
            }

            await smtp.EnviarAsync(smtp.AprovaReprova(varejista.Usuario?.Email, varejista.Status));
        }

        public async Task AdicionarOuAlterarEnderecoLogadoAsync(string cep, string logradouro, string numero, string complemento, string bairro, string cidade, string estado)
        {
            var id = me.FindFirst(ClaimsConstant.Id).Value;

            var varejista = await varejistaRepository.PorAsync(c => c.UsuarioId == id, noTracking: false);

            Validar(varejista == null,
                "Usuário não encontrado, talvez a sessão tenha expirado, tente logar novamente");

            if (varejista.Endereco is null)
            {
                varejista.Endereco = new Endereco(cep, logradouro, numero, complemento, bairro, cidade, estado);
            }
            else
            {
                varejista.Endereco.Cep = cep;
                varejista.Endereco.Logradouro = logradouro;
                varejista.Endereco.Numero = numero;
                varejista.Endereco.Complemento = complemento;
                varejista.Endereco.Bairro = bairro;
                varejista.Endereco.Cidade = cidade;
                varejista.Endereco.Estado = estado;

                varejista.Endereco.Validar();
            }

            await varejistaRepository.AtualizarAsync(varejista);
        }

        public async Task<EntidadeDto> PorIdAsync(string id)
        {
            return EntidadeDto.From(await varejistaRepository.PorIdAsync(id));
        }

        public async Task<EntidadeDto> MeusDadosAsync()
        {
            var id = me.FindFirst(ClaimsConstant.Id).Value;

            Validar(string.IsNullOrEmpty(id),
               "Usuário não encontrado, talvez a sessão tenha expirado, tente logar novamente");

            var entidade = await varejistaRepository.PorAsync
            (
                varejista =>
                varejista.Usuario.Id == id,
                includes: i => i.Usuario
            );

            return EntidadeDto.From(entidade);
        }

        public async Task<IEnumerable<ContadorDto>> TotalAsync()
        {
            return await varejistaRepository.Queryable().GroupBy(x => x.Status).Select(a => new ContadorDto
            {
                Titulo = $"Varejistas com status {a.Key.ToString().ToLower()}",
                Status = a.Key.ToString(),
                Total = a.Count(/*s => s.Status == a.Key*/),
            }).ToListAsync();
        }

        public async Task<IEnumerable<EntidadeDto>> ListarPorStatusAsync(RegistroStatus status)
        {
            return (await varejistaRepository.ListarPorAsync(c => c.Status == status))
                .OrderBy(a => a.CriadoEm).Select(EntidadeDto.From);
        }
    }
}