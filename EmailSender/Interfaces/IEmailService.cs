using System.Threading.Tasks;

namespace EmailSender.Interfaces
{
    interface IEmailService
    {
        public Task SendEmailAsync(IEmailSettings emailSettings, IMessage message, ISiteSettings siteSettings);
    }
}
