using System.ComponentModel.DataAnnotations;

namespace IdentityServer4.IdentityCore.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
