using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Core.ViewModels
{
    public class PasswordChangeViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanı boş olamaz")]
        [Display(Name = "Şifre:")]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter olabilir")]
        public string PasswordOld { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Yeni Şifre alanı boş olamaz")]
        [Display(Name = "Şifre:")]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter olabilir")]
        public string PasswordNew { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare(nameof(PasswordNew), ErrorMessage = "Şifreler Aynı değildir.")]
        [Required(ErrorMessage = "Yeni Şifre Tekrar alanı boş olamaz")]
        [Display(Name = "Yeni Şifre Tekrar:")]
        [MinLength(6, ErrorMessage ="Şifreniz en az 6 karakter olabilir")]
        public string PasswordNewConfirm { get; set; } = null!;
    }
}
