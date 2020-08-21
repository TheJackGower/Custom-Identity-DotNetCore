using CustomNetCoreIdentity.Domain.Entities.Shared;

namespace CustomNetCoreIdentity.Domain.Entities
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
