using mercadosuspenso.domain.Dtos;
using System.Threading.Tasks;

namespace mercadosuspenso.domain.Interfaces.Services
{
    public interface IAutenticacaoService
    {
        Task<SignInDto> SignInAdminAsync(string email, string password);
        Task<SignInDto> SignInVarejistaAsync(string cnpj, string password);
        Task<SignInDto> SignInDistribuidorAsync(string cnpj, string password);
    }
}