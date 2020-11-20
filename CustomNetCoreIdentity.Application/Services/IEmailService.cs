using System.Threading.Tasks;

namespace CustomNetCoreIdentity.Application.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
