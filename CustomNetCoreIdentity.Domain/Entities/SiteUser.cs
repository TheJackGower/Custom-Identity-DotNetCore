using CustomNetCoreIdentity.Domain.Entities.Shared;
using System.ComponentModel.DataAnnotations;

namespace CustomNetCoreIdentity.Domain.Entities
{
    public class SiteUser : BaseEntity
    {
        /// <summary>
        /// User identifier. No longer using Guids as uneeded memory is consumed in creation
        /// </summary>
        [Display(Name = "ID")]
        public int Id { get; set; }

        public string DisplayName => $"{Forename} {Surname}";

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

        public string Facebook { get; set; }

        public string Twitter { get; set; }

        public string Instagram { get; set; }

        public string Website { get; set; }
    }
}
