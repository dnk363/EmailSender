using EmailSender.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace EmailSender.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(IEmailSettings emailSettings, IMessage message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(message.SenderName, message.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", message.RecieverEmail));
            emailMessage.Subject = message.MessageSubject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message.MessageBody
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(emailSettings.Host, emailSettings.Port, emailSettings.EnableSSL);
                await client.AuthenticateAsync(emailSettings.UserName, emailSettings.UserPassword);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
