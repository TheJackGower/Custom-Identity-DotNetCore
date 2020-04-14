using IdentityManager.Entities.Shared;

namespace IdentityManager.Entities.Custom
{
    public class SiteUser : BaseEntity
    {
        /// <summary>
        /// User identifier. No longer using Guids as uneeded memory is consumed in creation
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Typically their email
        /// </summary>
        public string Username { get; set; }

        public string NormalizedUserName { get; set; }

        public string Email { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public string NormalizedEmail { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }
    }
}
