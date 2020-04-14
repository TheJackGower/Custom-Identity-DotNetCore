using IdentityManager.Entities.Custom;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityManager.DAL
{
    public class RoleService
    {
        private IConfiguration _config { get; set; }

        public RoleService(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Retrieve role by the supplied name
        /// </summary>
        /// <param name="normalizedRoleName"></param>
        /// <returns>SiteRole</returns>
        public async Task<SiteRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            try
            {
                var role = new SiteRole();
                using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync(cancellationToken);

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "identity_FindRoleByName";

                        cmd.Parameters.AddWithValue("@NormalizedRoleName", normalizedRoleName);

                        using (var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                        {
                            if (rdr.Read())
                            {
                                role = new SiteRole
                                {
                                    Id = int.Parse(rdr["Id"].ToString()),
                                    Name = rdr["Name"].ToString(),
                                    NormalizedName = rdr["NormalizedName"].ToString(),
                                    Description = rdr["Description"].ToString(),
                                    Created = DateTime.Parse(rdr["Created"].ToString())
                                };
                            }
                            return role;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieve role by the supplied Id
        /// </summary>
        /// <param name="normalizedRoleName"></param>
        /// <returns>SiteRole</returns>
        public async Task<SiteRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            try
            {
                var role = new SiteRole();
                using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync(cancellationToken);

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "identity_FindRoleById";

                        cmd.Parameters.AddWithValue("@Id", roleId);

                        using (var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow, cancellationToken))
                        {
                            if (rdr.Read())
                            {
                                role = new SiteRole
                                {
                                    Id = int.Parse(rdr["Id"].ToString()),
                                    Name = rdr["Name"].ToString(),
                                    NormalizedName = rdr["NormalizedName"].ToString(),
                                    Description = rdr["Description"].ToString(),
                                    Created = DateTime.Parse(rdr["Created"].ToString())
                                };
                            }
                            return role;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns a list of roles the user is in as strings
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Roles</returns>
        public async Task<IList<string>> GetRolesByUserIdAsync(SiteUser user, CancellationToken cancellationToken)
        {
            // IUserStore requires a IQueryable list
            var list = new List<string>();

            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync(cancellationToken);

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "identity_GetUserRolesByUserId";

                        cmd.Parameters.AddWithValue("@UserId", user.Id);

                        SqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            list.Add(rdr["RoleName"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("An Error Occurred!");
            }

            return list;
        }


    }
}
