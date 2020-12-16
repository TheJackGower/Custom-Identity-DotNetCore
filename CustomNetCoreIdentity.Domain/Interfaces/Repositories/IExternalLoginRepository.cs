using CustomNetCoreIdentity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomNetCoreIdentity.Domain.Interfaces.Repositories
{
    public interface IExternalLoginRepository
    {
        Task<List<UserLoginInfo>> ListForUserId(SiteUser user, CancellationToken cancellationToken);

        Task CreateExternalLoginUser(SiteUser user, UserLoginInfo login, CancellationToken cancellationToken);

        Task<int> GetUserIdByLoginProvider(string loginProvider, string providerKey, CancellationToken cancellationToken);

        Task RemoveLogin(SiteUser user, string loginProvider, string providerKey, CancellationToken cancellationToken);
    }
}