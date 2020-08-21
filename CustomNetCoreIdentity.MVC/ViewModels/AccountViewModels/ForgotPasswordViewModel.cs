using System.ComponentModel.DataAnnotations;

namespace CustomNetCoreIdentity.MVC.ViewModels.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
