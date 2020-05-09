using mercadosuspenso.domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mercadosuspenso.domain.Interfaces.Services
{
    public interface IEstoqueService
    {
        Task<ProcessamentoDto> EntradaAsync(string chave, int versao, int ambiente, int identificador, string hash);
        Task ResgatarAsync(string hashCode, string distribuidorId);
        Task RetirarAsync(string hashCode, string cpf);

        Task<IEnumerable<ContadorDto>> TotalAsync();
        Task<IEnumerable<VistoriaDto>> EstoqueLogadoAsync();

        Task<IEnumerable<DoacaoDto>> EstoquePorVarejistaIdAsync(string id);
        Task<IEnumerable<DoacaoDto>> EstoquePorDistribuidorIdAsync(string id);
    }
}