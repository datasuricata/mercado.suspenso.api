using mercadosuspenso.domain.Dtos;
using mercadosuspenso.domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mercadosuspenso.domain.Interfaces.Services
{
    public interface IVarejistaService
    {
        Task AdicionarAsync(string razaoSocial, string nome, string cnpj, string telefone, string email, string senha);
        Task AprovarRecusarAsync(string id);
        Task AdicionarOuAlterarEnderecoLogadoAsync(string cep, string logradouro, string numero, string complemento, string bairro, string cidade, string estado);
        Task AtualizarAsync(string razaoSocial, string representante, string cnpj, string telefone);

        Task<EntidadeDto> PorIdAsync(string id);
        Task<EntidadeDto> MeusDadosAsync();

        Task<IEnumerable<ContadorDto>> TotalAsync();
        Task<IEnumerable<EntidadeDto>> ListarPorStatusAsync(RegistroStatus status);
    }
}