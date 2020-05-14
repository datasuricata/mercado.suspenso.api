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
        private readonly IRepository<Varejista> repository;
        private readonly ClaimsPrincipal me;

        public VarejistaService(ISmtpService smtp, IPrincipal principal, IRepository<Varejista> repository)
        {
            this.smtp = smtp;
            this.repository = repository;
            this.me = (ClaimsPrincipal)principal;
        }

        public async Task AdicionarAsync(string razaoSocial, string representante, string cnpj, string telefone, string email, string senha)
        {
            var varejista = new Varejista(razaoSocial, representante, cnpj, telefone)
            {
                Usuario = new Usuario(email, senha),
            };

            varejista.Usuario.DefinirTipo(UsuarioTipo.Varejo);

            var registrado = await repository.ByAsync(p => p.Ativo && p.Cnpj == varejista.Cnpj);

            if (registrado != null)
            {
                Validar(registrado.Status == RegistroStatus.Recusado,
                    "Existem algum problema com seu cadastro, contate o suporte");

                Validar(registrado != null,
                    "Já um cadastro processado ou pendente com estes dados");
            }

            await repository.InsertAsync(varejista);

            await smtp.EnviarAsync(smtp.BoasVindas(email, representante));
        }

        public async Task AtualizarAsync(string razaoSocial, string representante, string cnpj, string telefone)
        {
            var id = me.FindFirst(ClaimsConstant.Id).Value;

            var varejista = await repository.ByAsync(c => c.UsuarioId == id, false);

            Validar(varejista == null,
                "Usuário não encontrado, talvez a sessão tenha expirado, tente logar novamente");

            varejista.RazaoSocial = razaoSocial;
            varejista.Representante = representante;
            varejista.Cnpj = cnpj;
            varejista.Telefone = telefone;

            await repository.UpdateAsync(varejista);
        }

        public async Task AprovarRecusarAsync(string id)
        {
            var varejista = await repository.ByAsync(p => p.Ativo && p.Id == id, false, i => i.Usuario);

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

            var varejista = await repository.ByAsync(c => c.UsuarioId == id, false);

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

            await repository.UpdateAsync(varejista);
        }

        public async Task<EntidadeDto> PorIdAsync(string id)
        {
            var entidade = await repository.ByIdAsync(id);

            return EntidadeDto.From(entidade);
        }

        public async Task<EntidadeDto> MeusDadosAsync()
        {
            var id = me.FindFirst(ClaimsConstant.Id).Value;

            Validar(string.IsNullOrEmpty(id),
               "Usuário não encontrado, talvez a sessão tenha expirado, tente logar novamente");

            var entidade = await repository.ByAsync
            (
                varejista =>
                varejista.Usuario.Id == id,
                readOnly: true,
                includes: i => i.Usuario
            );

            return EntidadeDto.From(entidade);
        }

        public async Task<IEnumerable<ContadorDto>> TotalAsync()
        {
            return await repository.Queryable(true).GroupBy(x => x.Status).Select(a => new ContadorDto
            {
                Titulo = $"Varejistas com status {a.Key.ToString().ToLower()}",
                Status = a.Key.ToString(),
                Total = a.Count(/*s => s.Status == a.Key*/),
            }).ToListAsync();
        }

        public async Task<IEnumerable<EntidadeDto>> ListarPorStatusAsync(RegistroStatus status)
        {
            var entidades = await repository.ListByAsync(c => c.Status == status, noTracking: true);

            return entidades.OrderBy(a => a.CriadoEm).Select(EntidadeDto.From);
        }
    }
}