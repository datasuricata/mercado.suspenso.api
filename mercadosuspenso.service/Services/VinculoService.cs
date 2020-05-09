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
    public class VinculoService : IVinculoService
    {
        private delegate void Assert(bool error, string message);
        private Assert Validar = DomainException.Validate;

        private readonly IRepository<Vinculo> repository;
        private readonly ClaimsPrincipal me;

        public VinculoService(IRepository<Vinculo> repository, IPrincipal principal)
        {
            this.repository = repository;
            this.me = (ClaimsPrincipal)principal;
        }

        public async Task VincularParceirosAsync(UsuarioTipo tipo, string parceiroId, List<string> parceirosIds)
        {
            var userId = me.FindFirst(ClaimsConstant.Id).Value;

            var vinculos = default(IEnumerable<Vinculo>);

            Validar(tipo == default || tipo == UsuarioTipo.Administrador,
                "Tipo de parceiro invalido");

            Validar(string.IsNullOrEmpty(parceiroId),
                "Informe o identificador do parceiro que vai receber os vinculos");

            Validar(parceirosIds == null,
                "Informar os parceiros que vão ser vinculados é obrigatório");

            Validar(parceirosIds.Count() == 0,
                "Deve conter ao menos um parceiro na lista");

            if (tipo == UsuarioTipo.Varejo)
                vinculos = parceirosIds.Select(i => new Vinculo(i, parceiroId) { UsuarioId = userId });

            if (tipo == UsuarioTipo.Distribuidor)
                vinculos = parceirosIds.Select(i => new Vinculo(parceiroId, i) { UsuarioId = userId });

            await repository.InsertRangeAsync(vinculos);
        }

        public async Task RemoverVinculosAsync(string parceiroid, List<string> parceirosIds)
        {
            var userId = me.FindFirst(ClaimsConstant.Id).Value;

            Validar(string.IsNullOrEmpty(parceiroid),
                "Informe o identificador do parceiro que vai receber os vinculos");

            Validar(parceirosIds == null,
                "Informar os identificadores dos vinculos");

            Validar(parceirosIds.Count() == 0,
                "Deve conter ao menos um vinculo na lista");

            var vinculos = await repository.ListByAsync(vinculo => parceirosIds.Any(a => a == vinculo.Id), false);

            if (vinculos != null)
            {
                foreach (var vinculo in vinculos)
                {
                    vinculo.Ativo = false;
                    vinculo.UsuarioId = userId;

                    await repository.UpdateAsync(vinculo);
                }
            }
        }

        public async Task<IEnumerable<EntidadeDto>> ListarVarejistaSemVinculoPorDistribuidor(string distribuidorId, RegistroStatus status)
        {
            var query = repository.Queryable(noTracking: true, includes: i => i.Varejista);

            var vinculos = await query.Where
            (
                vinculo => 
                vinculo.DistribuidorId != distribuidorId && 
                vinculo.Ativo && 
                vinculo.Distribuidor.Status == status
            )
            .GroupBy(vinculo => vinculo.Varejista).Distinct().Select(grupo => grupo.Key).ToListAsync();

            return vinculos.Select(EntidadeDto.From);
        }

        public async Task<IEnumerable<EntidadeDto>> ListarDistribuidorSemVinculoPorVarejista(string varejistaId, RegistroStatus status)
        {
            var query = repository.Queryable(noTracking: true, includes: i => i.Varejista);

            var vinculos = await query.Where
            (
                vinculo => 
                vinculo.VarejistaId != varejistaId && 
                vinculo.Ativo && 
                vinculo.Distribuidor.Status == status
            )
            .GroupBy(vinculo => vinculo.Distribuidor).Distinct().Select(grupo => grupo.Key).ToListAsync();

            return vinculos.Select(EntidadeDto.From);
        }

        public async Task<IEnumerable<VinculoDto>> ListarPorDistribuidorAsync(string distribuidorId)
        {
            Validar(string.IsNullOrEmpty(distribuidorId),
                "Informe o distribuidor para listar parceiros vinculados");

            var vinculos = await repository.ListByAsync
            (
                vinculo =>
                vinculo.Ativo &&
                vinculo.DistribuidorId == distribuidorId,
                noTracking: true,
                includes: i => i.Varejista
            );

            return vinculos.Select(VinculoDto.From);
        }

        public async Task<IEnumerable<VinculoDto>> ListarPorVarejistaAsync(string varejistaId)
        {
            Validar(string.IsNullOrEmpty(varejistaId),
                "Informe o varejista para listar parceiros vinculados");

            var vinculos = await repository.ListByAsync
            (
                vinculo =>
                vinculo.Ativo &&
                vinculo.DistribuidorId == varejistaId,
                noTracking: true,
                includes: i => i.Distribuidor
            );

            return vinculos.Select(VinculoDto.From);
        }

        public async Task<IEnumerable<VinculoDto>> ListarPorUsuarioLoggadoAsync()
        {
            var vinculos = default(IEnumerable<Vinculo>);
            var id = me.FindFirst(ClaimsConstant.Id).Value;
            var profileId = int.Parse(me.FindFirst(ClaimsConstant.ProfileId)?.Value);
            var tipo = (UsuarioTipo)profileId;

            Validar(tipo == UsuarioTipo.Administrador,
                "Endpoint com uso exclusivo dos parceiros");

            if (tipo == UsuarioTipo.Distribuidor)
            {
                vinculos = await repository.ListByAsync
                (
                    vinculo =>
                    vinculo.Ativo &&
                    vinculo.DistribuidorId == id,
                    noTracking: true,
                    includes: i => i.Varejista
                );
            }

            if (tipo == UsuarioTipo.Varejo)
            {
                vinculos = await repository.ListByAsync
                (
                    vinculo =>
                    vinculo.Ativo &&
                    vinculo.VarejistaId == id,
                    noTracking: true,
                    includes: i => i.Distribuidor
                );
            }

            return vinculos.Select(VinculoDto.From);
        }
    }
}
