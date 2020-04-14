using System.Threading.Tasks;

namespace CustomIdentityWEB.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
