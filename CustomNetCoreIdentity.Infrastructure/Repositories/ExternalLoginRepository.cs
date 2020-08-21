using CustomNetCoreIdentity.Domain.Entities;
using CustomNetCoreIdentity.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace CustomNetCoreIdentity.Infrastructure.Repositories
{
    public class ExternalLoginRepository : IExternalLoginRepository
    {
        private IConfiguration _config { get; set; }

        public ExternalLoginRepository(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Create new SiteUser from a external login (i.e. facebook twitter)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Success or Fail</returns>
        public async Task CreateExternalLoginUser(SiteUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync(cancellationToken);

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "identity_InsertExternalLoginUser";

                        cmd.Parameters.AddWithValue("@UserId", user.Id);
                        cmd.Parameters.AddWithValue("@LoginProvider", login.LoginProvider);
                        cmd.Parameters.AddWithValue("@ProviderKey", login.ProviderKey);
                        cmd.Parameters.AddWithValue("@ProviderDisplayName", login.ProviderDisplayName);
                        cmd.Parameters.AddWithValue("@Created", DateTime.Now);

                        await cmd.ExecuteNonQueryAsync(cancellationToken);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Retrieve User Id by external provider details
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>User ID</returns>
        public async Task<int> GetUserIdByLoginProvider(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            int UserId = 0;
            try
            {
                var user = new SiteUser();
                using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync(cancellationToken);

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "identity_FindByExternalLogin";

                        cmd.Parameters.AddWithValue("@LoginProvider", loginProvider);
                        cmd.Parameters.AddWithValue("@ProviderKey", providerKey);

                        UserId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                return UserId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
