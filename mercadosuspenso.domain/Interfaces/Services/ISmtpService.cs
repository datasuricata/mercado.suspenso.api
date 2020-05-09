using mercadosuspenso.domain.Enums;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace mercadosuspenso.domain.Interfaces.Services
{
    public interface ISmtpService
    {
        Task<HttpStatusCode> EnviarAsync(MailMessage mail);

        MailMessage BoasVindas(string email, string nome);
        MailMessage TrocaSenha(string email, string nome, string hash);
        MailMessage ResgateDoacao(string email, string varejista, string hash);
        MailMessage AprovaReprova(string email, RegistroStatus status);
    }
}
