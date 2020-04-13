using IdentityManager.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityManager.Entities.Custom
{
    public class SiteRole : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }

        /// <summary>
        /// Describe the roles purpose etc...
        /// </summary>
        public string Description { get; set; }
    }
}
