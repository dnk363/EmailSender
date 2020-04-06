using System.Threading.Tasks;

namespace EmailSender.Interfaces
{
    interface IEmailService
    {
        public Task SendEmailAsync(IFormMessageService formMessageService, IEmailSettings emailSettings, IMessage message, ISiteSettings siteSettings);
    }
}
