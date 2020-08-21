using CustomNetCoreIdentity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace CustomNetCoreIdentity.Domain.Interfaces.Repositories
{
    public interface IExternalLoginRepository
    {
        Task CreateExternalLoginUser(SiteUser user, UserLoginInfo login, CancellationToken cancellationToken);
        Task<int> GetUserIdByLoginProvider(string loginProvider, string providerKey, CancellationToken cancellationToken);
    }
}