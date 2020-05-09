using mercadosuspenso.domain.Enums;
using mercadosuspenso.domain.Interfaces.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace mercadosuspenso.service.Services
{
    public class SendGridSmtpService : ISmtpService
    {
        private readonly ISendGridClient sendGridClient;
        private MailAddress ApplicationDefault => new MailAddress("contato@alimentocuritiba.com.br", "+Alimento Curitiba");

        public SendGridSmtpService(ISendGridClient sendGridClient)
        {
            this.sendGridClient = sendGridClient;
        }

        public async Task<HttpStatusCode> EnviarAsync(MailMessage mail)
        {
            var from = new EmailAddress(mail.From.Address, mail.From.DisplayName);

            var to = new EmailAddress(mail.To.First().Address, mail.To.First().DisplayName);

            var msg = MailHelper.CreateSingleEmail(from, to, mail.Subject, mail.Body, mail.Body);

            var response = await sendGridClient.SendEmailAsync(msg);

            return response.StatusCode;
        }

        public MailMessage BoasVindas(string email, string nome)
        {
            return new MailMessage(ApplicationDefault, new MailAddress(email, nome))
            {
                Subject = "+AlimentoCuritiba - Boas Vindas!",
                Body = $"Olá seja muito bem vindo {nome}! Seu cadastro passa por um processo de aprovação e assim que estiver com tudo pronto voltamos com novidades. Muito obrigado pelo seu apoio.",
            };
        }

        public MailMessage TrocaSenha(string email, string nome, string hash)
        {
            return new MailMessage(ApplicationDefault, new MailAddress(email, nome))
            {
                Subject = "+AlimentoCuritiba - Reset de Senha!",
                Body = $"{nome}! Utilize este código temporário {hash} para trocar sua senha.",
            };
        }

        public MailMessage ResgateDoacao(string email, string varejista, string hash)
        {
            return new MailMessage(ApplicationDefault, new MailAddress(email))
            {
                Subject = "+AlimentoCuritiba - Nova Arrecadação!",
                Body = $"Existe uma nova doação para retirada no varejista {varejista} próximo de você. Utilize o código {hash} para retirada do(s) produto(s)",
            };
        }

        public MailMessage AprovaReprova(string email, RegistroStatus status)
        {
            var subject = default(string);
            var body = default(string);

            switch (status)
            {
                case RegistroStatus.Pendente:
                    subject = "Cadastro em Análise";
                    body = "Informamos que seu cadastro esta passando por um processo de análise e auditoria, obrigado pela colaboração";
                    break;
                case RegistroStatus.Aprovado:
                    subject = "Cadastro Aprovado";
                    body = "Uhu! Informamos que seu cadastro foi 100% aprovado, e já podemos dar inicio nessa nova jornada juntos";
                    break;
                case RegistroStatus.Recusado:
                    subject = "Cadastro Recusado";
                    body = "Ops! Informamos que seu cadastro foi recusado, entre em contato com suporte para maiores informações";
                    break;
            }

            return new MailMessage(ApplicationDefault, new MailAddress(email))
            {
                Subject = $"+AlimentoCuritiba - {subject}!",
                Body = body,
            };
        }
    }
}
