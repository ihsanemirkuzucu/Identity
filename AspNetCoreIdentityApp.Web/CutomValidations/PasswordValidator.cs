using AspNetCoreIdentityApp.Repository.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.CutomValidations
{
    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            var errors = new List<IdentityError>();
            if(password!.ToLower().Contains(user.UserName!.ToLower()))
            {
                errors.Add(new()
                {
                    Code = "PasswordContainUserName",
                    Description = "Şifre alanı kullanıcı adı içeremez"
                });
            }
            if(password!.ToLower().StartsWith("1234"))
            {
                errors.Add(new()
                {
                    Code = "PasswordContain1234",
                    Description = "Şifre alanı 1234 ile başlayamaz"
                });
            }
            if (password!.ToLower().StartsWith("19"))
            {
                errors.Add(new()
                {
                    Code = "PasswordContain19",
                    Description = "Şifre alanı 19 ile başlayamaz"
                });
            }
            if (password!.ToLower().StartsWith("20"))
            {
                errors.Add(new()
                {
                    Code = "PasswordContain20",
                    Description = "Şifre alanı 20 ile başlayamaz"
                });
            }
            if(errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            return Task.FromResult(IdentityResult.Success);


        }
    }
}
