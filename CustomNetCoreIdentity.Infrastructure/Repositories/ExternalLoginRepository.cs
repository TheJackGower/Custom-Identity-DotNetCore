using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using CustomNetCoreIdentity.Domain.Entities;
using CustomNetCoreIdentity.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

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
        /// Retrieve a list of a users external logins by their user id
        /// </summary>
        /// <param name="user">The user object</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<List<UserLoginInfo>> ListForUserId(SiteUser user, CancellationToken cancellationToken)
        {
            var list = new List<UserLoginInfo>();

            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync(cancellationToken);

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Identity_ListExternalLoginByUserId";

                        cmd.Parameters.AddWithValue("@UserID", user.Id);

                        SqlDataReader rdr = await cmd.ExecuteReaderAsync(cancellationToken);

                        while (await rdr.ReadAsync(cancellationToken))
                        {
                            list.Add(new UserLoginInfo((string)rdr["LoginProvider"], (string)rdr["ProviderDisplayName"], (string)rdr["ProviderKey"]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return list;
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

        public async Task RemoveLogin(SiteUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
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
                        cmd.CommandText = "Identity_RemoveExternalLogin";

                        cmd.Parameters.AddWithValue("@UserID", user.Id);
                        cmd.Parameters.AddWithValue("@LoginProvider", loginProvider);
                        cmd.Parameters.AddWithValue("@ProviderKey", providerKey);

                        await cmd.ExecuteNonQueryAsync(cancellationToken);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
