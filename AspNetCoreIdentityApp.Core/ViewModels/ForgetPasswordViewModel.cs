using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Core.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "Email formatı yanlış!")]
        [Required(ErrorMessage = "Email alanı boş olamaz")]
        [Display(Name = "Email:")]
        public string Email { get; set; }
    }
}
