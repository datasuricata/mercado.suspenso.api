using mercadosuspenso.domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mercadosuspenso.domain.Interfaces.Services
{
    public interface IEstoqueService
    {
        Task<ProcessamentoDto> EntradaAsync(string chave, int versao, int ambiente, int identificador, string hash);
        Task<ProcessamentoDto> ResgatarAsync(string rastreio, string distribuidorId);
        Task<ProcessamentoDto> RetirarAsync(string rastreio, string cpf);

        Task<IEnumerable<ContadorDto>> TotalAsync(string distribuidorId);
        Task<IEnumerable<VistoriaDto>> EstoqueLogadoAsync();

        Task<IEnumerable<DoacaoDto>> EstoquePorVarejistaIdAsync(string varejistaId);
        Task<IEnumerable<DoacaoDto>> EstoquePorDistribuidorIdAsync(string distribuidorId);
    }
}