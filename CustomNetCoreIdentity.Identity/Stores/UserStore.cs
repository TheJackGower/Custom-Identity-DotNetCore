using CustomNetCoreIdentity.Domain.Entities;
using CustomNetCoreIdentity.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CustomNetCoreIdentity.Stores
{
    public class UserStore : IUserStore<SiteUser>,
                             IUserEmailStore<SiteUser>,
                             IUserPhoneNumberStore<SiteUser>,
                             IUserTwoFactorStore<SiteUser>,
                             IUserPasswordStore<SiteUser>,
                             IUserRoleStore<SiteUser>,
                             IUserLoginStore<SiteUser>,
                             IQueryableUserStore<SiteUser>
    {
        private ISiteUserRepository siteUserRepository { get; set; }

        private ISiteRoleRepository _siteRoleRepository { get; set; }

        private IUserRoleRepository _userRoleRepository { get; set; }

        private IExternalLoginRepository _externalLoginRepository { get; set; }

        public UserStore(ISiteUserRepository siteUserRepository, ISiteRoleRepository siteRoleRepository, IUserRoleRepository userRoleRepository, IExternalLoginRepository externalLoginRepository)
        {
            this.siteUserRepository = siteUserRepository;
            _siteRoleRepository = siteRoleRepository;
            _externalLoginRepository = externalLoginRepository;
            _userRoleRepository = userRoleRepository;
        }

        public IQueryable<SiteUser> Users => siteUserRepository.GetUserList();

        public async Task<IdentityResult> CreateAsync(SiteUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await siteUserRepository.CreateUser(user, cancellationToken);
        }

        public async Task<IdentityResult> DeleteAsync(SiteUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await siteUserRepository.DeleteUser(user, cancellationToken);
        }

        public async Task<SiteUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await siteUserRepository.FindById(userId, cancellationToken);
        }

        public async Task<SiteUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await siteUserRepository.FindByName(normalizedUserName, cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(SiteUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(SiteUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(SiteUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Username);
        }

        public Task SetNormalizedUserNameAsync(SiteUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(SiteUser user, string userName, CancellationToken cancellationToken)
        {
            user.Username = userName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(SiteUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await siteUserRepository.UpdateUser(user, cancellationToken);
        }

        public Task SetEmailAsync(SiteUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(SiteUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(SiteUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(SiteUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public async Task<SiteUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await siteUserRepository.FindByEmail(normalizedEmail, cancellationToken);
        }

        public Task<string> GetNormalizedEmailAsync(SiteUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(SiteUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberAsync(SiteUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(SiteUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(SiteUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(SiteUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task SetTwoFactorEnabledAsync(SiteUser user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(SiteUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        #region Roles

        public async Task AddToRoleAsync(SiteUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = await _siteRoleRepository.FindByNameAsync(roleName.ToUpper(), cancellationToken);

            if (role == null)
            {
                // No role found, so create one...
                //        roleId = await connection.ExecuteAsync($"INSERT INTO [SiteRole]([Name], [NormalizedName]) VALUES(@{nameof(roleName)}, @{nameof(normalizedName)})",
                //            new { roleName, normalizedName });
                throw new Exception("No Role Found by Name!");
            }

            await _userRoleRepository.AddUserToRoleAsync(user.Id, role.Id, cancellationToken);
        }

        public async Task RemoveFromRoleAsync(SiteUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // Retrieve role to get id
            var role = await _siteRoleRepository.FindByNameAsync(roleName.ToUpper(), cancellationToken);

            if (role == null)
            {
                throw new Exception($"Couldn't retrieve role with name - {role}");
            }

            await _userRoleRepository.RemoveUserFromRole(user.Id, role.Id, cancellationToken);
        }

        public async Task<IList<string>> GetRolesAsync(SiteUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _siteRoleRepository.GetRolesByUserIdAsync(user, cancellationToken);
        }

        public async Task<bool> IsInRoleAsync(SiteUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // Retrieve role to get id
            var role = await _siteRoleRepository.FindByNameAsync(roleName.ToUpper(), cancellationToken);

            if (role == null)
            {
                return false;
            }

            return await _userRoleRepository.IsUserInRoleAsync(user.Id, role.Id, cancellationToken);
        }

        public async Task<IList<SiteUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _userRoleRepository.GetUsersInRoleAsync(roleName.ToUpper(), cancellationToken);
        }

        #endregion

        #region External Login

        public async Task AddLoginAsync(SiteUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _externalLoginRepository.CreateExternalLoginUser(user, login, cancellationToken);
        }

        public async Task<SiteUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            int UserId = await _externalLoginRepository.GetUserIdByLoginProvider(loginProvider, providerKey, cancellationToken);

            if (UserId == 0)
            {
                return null;
            }

            return await siteUserRepository.FindById(UserId.ToString(), cancellationToken);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(SiteUser user, CancellationToken cancellationToken)
        {
            return await _externalLoginRepository.ListForUserId(user, cancellationToken);
        }

        public async Task RemoveLoginAsync(SiteUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            // Called when deleting a user that was created from an external source
            await _externalLoginRepository.RemoveLogin(user, loginProvider, providerKey, cancellationToken);
        }

        #endregion

        #region Password

        public Task SetPasswordHashAsync(SiteUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(SiteUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(SiteUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        #endregion

        #region Security stamp
        public virtual Task<string> GetSecurityStampAsync(SiteUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.SecurityStamp);
        }

        public virtual Task SetSecurityStampAsync(SiteUser user, string stamp)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this != null)
                {
                    //this.Dispose();
                }
            }
        }

    }
}
