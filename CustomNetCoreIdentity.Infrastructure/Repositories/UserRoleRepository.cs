using CustomNetCoreIdentity.Domain.Entities;
using CustomNetCoreIdentity.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace CustomNetCoreIdentity.Infrastructure.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private IConfiguration _config { get; set; }

        public UserRoleRepository(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Adds a user by ID to role by ID
        /// Checks should be performed to ensure role exists before removing user
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="RoleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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
        /// Remove user from specified role
        /// Ensure role exists before removal
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="RoleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task RemoveUserFromRole(int UserId, int RoleId, CancellationToken cancellationToken)
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
                        cmd.CommandText = "identity_RemoveUserFromRole";

                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@RoleId", RoleId);

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
        /// Check if user is in the supplied role
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="RoleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> IsUserInRoleAsync(int UserId, int RoleId, CancellationToken cancellationToken)
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
                        cmd.CommandText = "identity_IsUserInRole";

                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@RoleId", RoleId);

                        var value = Convert.ToInt32(cmd.ExecuteScalar());

                        if (value > 0)
                        {
                            // Yes, in that role
                            return true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Get all users in the supplied role name
        /// </summary>
        /// <param name="NormalizedRoleName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<SiteUser>> GetUsersInRoleAsync(string NormalizedRoleName, CancellationToken cancellationToken)
        {
            var list = new List<SiteUser>();
            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync(cancellationToken);

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "identity_GetUsersInRoleByRoleName";

                        cmd.Parameters.AddWithValue("@NormalizedRoleName", NormalizedRoleName);

                        SqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            SiteUser su = new SiteUser
                            {
                                Id = int.Parse(rdr["Id"].ToString()),
                                Username = rdr["Username"].ToString(),
                                NormalizedUserName = rdr["NormalizedUserName"].ToString(),
                                Forename = rdr["Forename"].ToString(),
                                Surname = rdr["Surname"].ToString(),
                                Email = rdr["Email"].ToString(),
                                NormalizedEmail = rdr["NormalizedEmail"].ToString(),
                                EmailConfirmed = bool.Parse(rdr["EmailConfirmed"].ToString()),
                                PhoneNumber = rdr["PhoneNumber"].ToString(),
                                PhoneNumberConfirmed = bool.Parse(rdr["PhoneNumberConfirmed"].ToString()),
                                TwoFactorEnabled = bool.Parse(rdr["TwoFactorEnabled"].ToString()),
                                Created = DateTime.Parse(rdr["Created"].ToString())
                            };
                            list.Add(su);
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
