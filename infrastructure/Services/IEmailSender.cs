using System.Threading.Tasks;

namespace HPHR.Infrastructure.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlMessage, string textMessage = null);
    }
}
