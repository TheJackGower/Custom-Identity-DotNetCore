using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityManager.DAL
{
    public class UserRoleService
    {
        private IConfiguration _config { get; set; }

        public UserRoleService(IConfiguration config)
        {
            _config = config;
        }

        public async Task AddUserToRoleAsync(int UserId, int RoleId, CancellationToken cancellationToken)
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
                        cmd.CommandText = "identity_AddUserToRole";

                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@RoleId", RoleId);

                        await cmd.ExecuteNonQueryAsync();
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
