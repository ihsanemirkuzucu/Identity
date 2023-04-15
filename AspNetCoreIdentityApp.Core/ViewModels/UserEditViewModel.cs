using AspNetCoreIdentityApp.Core.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Core.ViewModels
{
    public class UserEditViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı alanı boş olamaz")]
        [Display(Name = "Kullanıcı Adı:")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "Email formatı yanlış!")]
        [Required(ErrorMessage = "Email alanı boş olamaz")]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon alanı boş olamaz")]
        [Display(Name = "Telefon Numarası:")]
        public string Phone { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Doğum Tarihi:")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Şehir:")]
        public string? City { get; set; }

        [Display(Name = "Profil Fotoğrafı:")]
        public IFormFile? Picture { get; set; }

        [Display(Name = "Cinsiyet:")]
        public Gender? Gender { get; set; }

    }
}
