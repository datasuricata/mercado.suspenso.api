using mercadosuspenso.domain.Dtos;
using mercadosuspenso.domain.Enums;
using mercadosuspenso.domain.Exceptions;
using mercadosuspenso.domain.Interfaces.Services;
using mercadosuspenso.domain.Models;
using mercadosuspenso.domain.Security;
using mercadosuspenso.orm.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace mercadosuspenso.service.Services
{
    public class DistribuidorService : IDistribuidorService
    {
        private delegate void Assert(bool error, string message);
        private Assert Validar = DomainException.Validate;

        private readonly ISmtpService smtp;
        private readonly IRepository<Distribuidor> repository;
        private readonly ClaimsPrincipal me;

        public DistribuidorService(ISmtpService smtp, IPrincipal principal, IRepository<Distribuidor> repository)
        {
            this.smtp = smtp;
            this.repository = repository;
            this.me = (ClaimsPrincipal)principal;
        }

        public async Task AdicionarAsync(string razaoSocial, string representante, string cnpj, string telefone, string email, string senha)
        {
            var distribuidor = new Distribuidor(razaoSocial, representante, cnpj, telefone)
            {
                Usuario = new Usuario(email, senha),
            };

            distribuidor.Usuario.DefinirTipo(UsuarioTipo.Distribuidor);

            var registrado = await repository.ByAsync(p => p.Ativo && p.Cnpj == distribuidor.Cnpj);

            if (registrado != null)
            {
                Validar(registrado.Status == RegistroStatus.Recusado,
                    "Existem algum problema com seu cadastro, contate o suporte");

                Validar(registrado != null,
                    "Já um cadastro processado ou pendente com estes dados");
            }

            await repository.InsertAsync(distribuidor);

            await smtp.EnviarAsync(smtp.BoasVindas(email, representante));
        }

        public async Task AtualizarAsync(string razaoSocial, string representante, string cnpj, string telefone)
        {
            var id = me.FindFirst(ClaimsConstant.Id).Value;

            var distribuidor = await repository.ByAsync(c => c.UsuarioId == id, false);

            Validar(distribuidor == null,
                "Usuário não encontrado, talvez a sessão tenha expirado, tente logar novamente");

            distribuidor.RazaoSocial = razaoSocial;
            distribuidor.Representante = representante;
            distribuidor.Cnpj = cnpj;
            distribuidor.Telefone = telefone;

            await repository.UpdateAsync(distribuidor);
        }

        public async Task AprovarRecusarAsync(string id)
        {
            var distribuidor = await repository.ByAsync(p => p.Ativo && p.Id == id, false, i => i.Usuario);

            if (distribuidor.Status == RegistroStatus.Aprovado)
            {
                distribuidor.Recusar();
            }
            if (distribuidor.Status == RegistroStatus.Pendente || distribuidor.Status == RegistroStatus.Recusado)
            {
                distribuidor.Aprovar();
            }

            await smtp.EnviarAsync(smtp.AprovaReprova(distribuidor.Usuario?.Email, distribuidor.Status));
        }

        public async Task AdicionarOuAlterarEnderecoLogadoAsync(string cep, string logradouro, string numero, string complemento, string bairro, string cidade, string estado)
        {
            var id = me.FindFirst(ClaimsConstant.Id).Value;

            var distribuidor = await repository.ByAsync(c => c.UsuarioId == id, false);

            Validar(distribuidor == null,
                "Usuário não encontrado, talvez a sessão tenha expirado, tente logar novamente");

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
                distribuidor => 
                distribuidor.Usuario.Id == id,
                readOnly: true,
                includes: i => i.Usuario
            );

            return EntidadeDto.From(entidade);
        }

        public async Task<IEnumerable<ContadorDto>> TotalAsync()
        {
            return await repository.Queryable(noTracking: true)
                .Where(x => x.Ativo).GroupBy(x => x.Status).Select(a => new ContadorDto
                {
                    Titulo = $"Distribuidores com status {a.Key.ToString().ToLower()}",
                    Status = a.Key.ToString(),
                    Total = a.Count(/*s => s.Status == a.Key*/),
                }).ToListAsync();
        }

        public async Task<IEnumerable<EntidadeDto>> ListarPorStatusAsync(RegistroStatus status)
        {
            var entidades =  await repository.ListByAsync(c => c.Status == status, noTracking: true);

            return entidades.OrderBy(a => a.CriadoEm).Select(EntidadeDto.From);
        }
    }
}