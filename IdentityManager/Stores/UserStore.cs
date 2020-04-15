using IdentityManager.DAL;
using IdentityManager.Entities.Custom;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityManager.Stores
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
        private UserService _userService { get; set; }

        private RoleService _roleService { get; set; }

        private UserRoleService _userRoleService { get; set; }

        private ExternalLoginService _externalLoginService { get; set; }

        public UserStore(UserService userService, RoleService roleService, UserRoleService userRoleService, ExternalLoginService externalLoginService)
        {
            _userService = userService;
            _roleService = roleService;
            _externalLoginService = externalLoginService;
            _userRoleService = userRoleService;
        }

        public IQueryable<SiteUser> Users => _userService.GetUserList();

        public async Task<IdentityResult> CreateAsync(SiteUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _userService.CreateUser(user, cancellationToken);
        }

        public async Task<IdentityResult> DeleteAsync(SiteUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _userService.DeleteUser(user, cancellationToken);
        }

        public async Task<SiteUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _userService.FindById(userId, cancellationToken);
        }

        public async Task<SiteUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _userService.FindByName(normalizedUserName, cancellationToken);
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

            return await _userService.UpdateUser(user, cancellationToken);
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

            return await _userService.FindByEmail(normalizedEmail, cancellationToken);
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

        public void Dispose()
        {
            // Nothing to dispose.
        }

        #region Roles

        public async Task AddToRoleAsync(SiteUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = await _roleService.FindByNameAsync(roleName.ToUpper(), cancellationToken);

            if (role == null)
            {
                // No role found, so create one...
                //        roleId = await connection.ExecuteAsync($"INSERT INTO [SiteRole]([Name], [NormalizedName]) VALUES(@{nameof(roleName)}, @{nameof(normalizedName)})",
                //            new { roleName, normalizedName });
                throw new Exception("No Role Found by Name!");
            }

            await _userRoleService.AddUserToRoleAsync(user.Id, role.Id, cancellationToken);
        }

        public async Task RemoveFromRoleAsync(SiteUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = await _roleService.FindByNameAsync(roleName.ToUpper(), cancellationToken);

            if (role == null)
            {
                throw new Exception($"Couldn't retrieve role with name - {role}");
            }

            //        await connection.ExecuteAsync($"DELETE FROM [SiteUserRole] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}", new { userId = user.Id, roleId });
        }

        public async Task<IList<string>> GetRolesAsync(SiteUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _roleService.GetRolesByUserIdAsync(user, cancellationToken);
        }

        public async Task<bool> IsInRoleAsync(SiteUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return false;
            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    var roleId = await connection.ExecuteScalarAsync<int?>("SELECT [Id] FROM [SiteRole] WHERE [NormalizedName] = @normalizedName", new { normalizedName = roleName.ToUpper() });
            //    if (roleId == default(int)) return false;
            //    var matchingRoles = await connection.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM [SiteUserRole] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}",
            //        new { userId = user.Id, roleId });

            //    return matchingRoles > 0;
            //}
        }

        public async Task<IList<SiteUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return null;

            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    var queryResults = await connection.QueryAsync<SiteUser>("SELECT u.* FROM [SiteUser] u " +
            //        "INNER JOIN [SiteUserRole] ur ON ur.[UserId] = u.[Id] INNER JOIN [SiteRole] r ON r.[Id] = ur.[RoleId] WHERE r.[NormalizedName] = @normalizedName",
            //        new { normalizedName = roleName.ToUpper() });

            //    return queryResults.ToList();
            //}
        }

        #endregion

        #region External Login

        public async Task AddLoginAsync(SiteUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _externalLoginService.CreateExternalLoginUser(user, login, cancellationToken);
        }

        public async Task<SiteUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            int UserId = await _externalLoginService.GetUserIdByLoginProvider(loginProvider, providerKey, cancellationToken);

            if (UserId == 0)
            {
                return null;
            }

            return await _userService.FindById(UserId.ToString(), cancellationToken);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(SiteUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(SiteUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            // Should be called when deleting a user that was created from an external source
            throw new NotImplementedException();
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

    }
}
