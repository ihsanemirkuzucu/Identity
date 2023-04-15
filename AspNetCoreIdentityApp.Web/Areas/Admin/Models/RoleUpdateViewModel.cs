using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.Areas.Admin.Models
{
    public class RoleUpdateViewModel
    {
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "Role İsmi alanı boş olamaz")]
        [Display(Name = "Role İsmi :")]
        public string Name { get; set; } = null!;
    }
}
