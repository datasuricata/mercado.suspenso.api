using System.Threading.Tasks;

namespace mercadosuspenso.domain.Interfaces.Services
{
    public interface IParticipanteService
    {
        Task AdicionarAsync(string nome, string cpf, string rg, string telefone);
    }
}
