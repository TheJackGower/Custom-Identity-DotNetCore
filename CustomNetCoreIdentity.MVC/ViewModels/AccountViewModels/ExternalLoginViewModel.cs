using System.ComponentModel.DataAnnotations;

namespace CustomNetCoreIdentity.MVC.ViewModels.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }
    }
}
