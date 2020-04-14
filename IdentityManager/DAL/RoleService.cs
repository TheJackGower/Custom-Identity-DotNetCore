using IdentityManager.Entities.Custom;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
        public async Task<SiteRole> FindByNameAsync(string normalizedRoleName)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                return null;
                //return await connection.QuerySingleOrDefaultAsync<SiteRole>($@"SELECT * FROM [SiteRole]
                //    WHERE [NormalizedName] = @{nameof(normalizedRoleName)}", new { normalizedRoleName });
            }
        }

    }
}
