using FilmesApi.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace FilmesApi.Services
{
    public class EmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void EnviarEmail(string[] destinatario, string assunto, int usuarioId, string code)
        {
            Mensagem mensagem = new(destinatario, assunto, usuarioId, code);

            var mensagemDeEmail = CriarCorpoDoEmail(mensagem);
            Enviar(mensagemDeEmail);
        }

        private void Enviar(MimeMessage mensagemDeEmail)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(
                    configuration.GetValue<string>("EmailSettings:SmtpServer"),
                    configuration.GetValue<int>("EmailSettings:Port"), true);

                client.AuthenticationMechanisms.Remove("XOUATH2");
                client.Authenticate(
                    configuration.GetValue<string>("EmailSettings:From"),
                    configuration.GetValue<string>("EmailSettings:Password"));

                client.Send(mensagemDeEmail);
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }

        private MimeMessage CriarCorpoDoEmail(Mensagem mensagem)
        {
            var mensagemDeEmail = new MimeMessage();
            mensagemDeEmail.From.Add(new MailboxAddress(configuration.GetValue<string>("EmailSettings:From")));
            mensagemDeEmail.To.AddRange(mensagem.Destinatario);
            mensagemDeEmail.Subject = mensagem.Assunto;
            mensagemDeEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = mensagem.Conteudo
            };

            return mensagemDeEmail;

        }

    }
}
