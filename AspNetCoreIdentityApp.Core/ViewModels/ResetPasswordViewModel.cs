using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Core.ViewModels
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanı boş olamaz")]
        [Display(Name = "Yeni Şifre:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Şifreler Aynı değildir.")]
        [Required(ErrorMessage = "Şifre Tekrar alanı boş olamaz")]
        [Display(Name = "Yeni Şifre Tekrar:")]
        public string PasswordConfirm { get; set; }
    }
}
