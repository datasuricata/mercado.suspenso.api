using mercadosuspenso.domain.Dtos;
using mercadosuspenso.domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mercadosuspenso.domain.Interfaces.Services
{
    public interface IVinculoService
    {
        Task VincularParceirosAsync(UsuarioTipo tipo, string id, List<string> ids);
        Task RemoverVinculosAsync(string id, List<string> ids);

        Task<IEnumerable<VinculoDto>> ListarPorDistribuidorAsync(string id);
        Task<IEnumerable<VinculoDto>> ListarPorVarejistaAsync(string id);
        Task<IEnumerable<VinculoDto>> ListarPorUsuarioLoggadoAsync();

        Task<IEnumerable<EntidadeDto>> ListarVarejistaSemVinculoPorDistribuidor(string distribuidorId, RegistroStatus status);
        Task<IEnumerable<EntidadeDto>> ListarDistribuidorSemVinculoPorVarejista(string varejistaId, RegistroStatus status);
    }
}