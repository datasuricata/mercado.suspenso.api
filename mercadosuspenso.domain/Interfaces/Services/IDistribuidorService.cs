using System.Threading.Tasks;

namespace mercadosuspenso.domain.Interfaces.Services
{
    public interface IDistribuidorService
    {
        Task AdicionarAsync(string razaoSocial, string nome, string cnpj, string telefone, string email, string senha);
        Task AprovarRecusarAsync(string cnpj);
        Task AdicionarOuAlterarEnderecoLogadoAsync(string cep, string logradouro, string numero, string complemento, string bairro, string cidade, string estado);
    }
}