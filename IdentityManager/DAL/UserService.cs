using IdentityManager.Entities.Custom;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IdentityManager.DAL
{
    public class UserService
    {
        private IConfiguration _config { get; set; }

        public UserService(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Retrieve all users from the database
        /// </summary>
        /// <returns>List of Users (SiteUser)</returns>
        public IQueryable<SiteUser> GetUserList()
        {
            // IUserStore requires a IQueryable list
            var list = Enumerable.Empty<SiteUser>();

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                // Get list all
            }

            return list.AsQueryable();
        }

    }
}
