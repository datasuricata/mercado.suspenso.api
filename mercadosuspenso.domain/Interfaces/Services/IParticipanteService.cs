using mercadosuspenso.domain.Dtos;
using mercadosuspenso.domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mercadosuspenso.domain.Interfaces.Services
{
    public interface IParticipanteService
    {
        Task AdicionarAsync(string nome, string cpf, string rg, string telefone, string email);
        Task AprovarRecusarAsync(string cpf);
      
        Task<IEnumerable<ContadorDto>> TotalAsync();
        Task<IEnumerable<EntidadeDto>> ListarPorStatusAsync(RegistroStatus status);
     
        Task<EntidadeDto> PorIdAsync(string id);
    }
}
