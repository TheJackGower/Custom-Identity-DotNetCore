using IdentityManager.DAL;
using IdentityManager.Entities.Custom;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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

        public UserStore(UserService userService)
        {
            _userService = userService;
        }

        public IQueryable<SiteUser> Users => _userService.GetUserList();

        public Task<IdentityResult> CreateAsync(SiteUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                user.Id = await connection.QuerySingleAsync<int>($@"INSERT INTO [SiteUser] 

                    ([UserName], 
                    [NormalizedUserName], 
                    [Email],
                    [NormalizedEmail], 
                    [EmailConfirmed],
                    [FirstName],
                    [Surname],
                    [PasswordHash],
                    [PhoneNumber], 
                    [PhoneNumberConfirmed], 
                    [TwoFactorEnabled])
                    
                    VALUES (@{nameof(SiteUser.UserName)},
                    @{nameof(SiteUser.NormalizedUserName)}, 
                    @{nameof(SiteUser.Email)},
                    @{nameof(SiteUser.NormalizedEmail)},
                    @{nameof(SiteUser.EmailConfirmed)},
                    @{nameof(SiteUser.FirstName)},
                    @{nameof(SiteUser.Surname)},
                    @{nameof(SiteUser.PasswordHash)},
                    @{nameof(SiteUser.PhoneNumber)},
                    @{nameof(SiteUser.PhoneNumberConfirmed)},
                    @{nameof(SiteUser.TwoFactorEnabled)});

                    SELECT CAST(SCOPE_IDENTITY() as int)", user);
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(SiteUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($"DELETE FROM [SiteUser] WHERE [Id] = @{nameof(SiteUser.Id)}", user);
            }

            return IdentityResult.Success;
        }

        public async Task<SiteUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<SiteUser>($@"SELECT * FROM [SiteUser]
                    WHERE [Id] = @{nameof(userId)}", new { userId });
            }
        }

        public async Task<SiteUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<SiteUser>($@"SELECT * FROM [SiteUser]
                    WHERE [NormalizedUserName] = @{nameof(normalizedUserName)}", new { normalizedUserName });
            }
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
            return Task.FromResult(user.UserName);
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

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($@"UPDATE [SiteUser] SET
                    [UserName] = @{nameof(SiteUser.Username)},
                    [NormalizedUserName] = @{nameof(SiteUser.NormalizedUserName)},
                    [Email] = @{nameof(SiteUser.Email)},
                    [NormalizedEmail] = @{nameof(SiteUser.NormalizedEmail)},
                    [EmailConfirmed] = @{nameof(SiteUser.EmailConfirmed)},
                    [FirstName] = @{nameof(SiteUser.Forename)},
                    [Surname] = @{nameof(SiteUser.Surname)},
                    [PasswordHash] = @{nameof(SiteUser.PasswordHash)},
                    [PhoneNumber] = @{nameof(SiteUser.PhoneNumber)},
                    [PhoneNumberConfirmed] = @{nameof(SiteUser.PhoneNumberConfirmed)},
                    [TwoFactorEnabled] = @{nameof(SiteUser.TwoFactorEnabled)}
                    WHERE [Id] = @{nameof(SiteUser.Id)}", user);
            }

            return IdentityResult.Success;
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

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<SiteUser>($@"SELECT * FROM [SiteUser]
                    WHERE [NormalizedEmail] = @{nameof(normalizedEmail)}", new { normalizedEmail });
            }
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

        public async Task AddToRoleAsync(SiteUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var normalizedName = roleName.ToUpper();
                var roleId = await connection.ExecuteScalarAsync<int?>($"SELECT [Id] FROM [ApplicationRole] WHERE [NormalizedName] = @{nameof(normalizedName)}", new { normalizedName });
                if (!roleId.HasValue)
                    roleId = await connection.ExecuteAsync($"INSERT INTO [ApplicationRole]([Name], [NormalizedName]) VALUES(@{nameof(roleName)}, @{nameof(normalizedName)})",
                        new { roleName, normalizedName });

                await connection.ExecuteAsync($"IF NOT EXISTS(SELECT 1 FROM [SiteUserRole] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}) " +
                    $"INSERT INTO [SiteUserRole]([UserId], [RoleId]) VALUES(@userId, @{nameof(roleId)})",
                    new { userId = user.Id, roleId });
            }
        }

        public async Task RemoveFromRoleAsync(SiteUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var roleId = await connection.ExecuteScalarAsync<int?>("SELECT [Id] FROM [ApplicationRole] WHERE [NormalizedName] = @normalizedName", new { normalizedName = roleName.ToUpper() });
                if (!roleId.HasValue)
                    await connection.ExecuteAsync($"DELETE FROM [SiteUserRole] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}", new { userId = user.Id, roleId });
            }
        }

        public async Task<IList<string>> GetRolesAsync(SiteUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var queryResults = await connection.QueryAsync<string>("SELECT r.[Name] FROM [ApplicationRole] r INNER JOIN [SiteUserRole] ur ON ur.[RoleId] = r.Id " +
                    "WHERE ur.UserId = @userId", new { userId = user.Id });

                return queryResults.ToList();
            }
        }

        public async Task<bool> IsInRoleAsync(SiteUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                var roleId = await connection.ExecuteScalarAsync<int?>("SELECT [Id] FROM [ApplicationRole] WHERE [NormalizedName] = @normalizedName", new { normalizedName = roleName.ToUpper() });
                if (roleId == default(int)) return false;
                var matchingRoles = await connection.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM [SiteUserRole] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}",
                    new { userId = user.Id, roleId });

                return matchingRoles > 0;
            }
        }

        public async Task<IList<SiteUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                var queryResults = await connection.QueryAsync<SiteUser>("SELECT u.* FROM [SiteUser] u " +
                    "INNER JOIN [SiteUserRole] ur ON ur.[UserId] = u.[Id] INNER JOIN [ApplicationRole] r ON r.[Id] = ur.[RoleId] WHERE r.[NormalizedName] = @normalizedName",
                    new { normalizedName = roleName.ToUpper() });

                return queryResults.ToList();
            }
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }

        #region External Login

        public async Task AddLoginAsync(SiteUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                await connection.ExecuteAsync($@"INSERT INTO [ExternalLogin] 
                ([LoginProvider], 
                 [ProviderKey], 
                 [ProviderDisplayName],
                 [UserID],
                 [Created])

                 VALUES (@{nameof(login.LoginProvider)},
                        @{nameof(login.ProviderKey)}, 
                        @{nameof(login.ProviderDisplayName)},
                        @{nameof(user.Id)}, 
                        @{nameof(DateTime.Now)});",

                        new
                        {
                            login.LoginProvider,
                            login.ProviderKey,
                            login.ProviderDisplayName,
                            user.Id,
                            DateTime.Now
                        });
            }
        }

        public async Task<SiteUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                var id = await connection.QuerySingleOrDefaultAsync<int>($@"SELECT UserID FROM [ExternalLogin]
                    WHERE [LoginProvider] = @{nameof(loginProvider)}
                    AND [ProviderKey] = @{nameof(providerKey)}", new { loginProvider, providerKey });



                var user = await FindByIdAsync(id.ToString(), cancellationToken);



                return user;
            }
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(SiteUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(SiteUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
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
