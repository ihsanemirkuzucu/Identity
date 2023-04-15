using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.Areas.Admin.Models
{
    public class RoleCreateViewModel
    {
        [Required(ErrorMessage = "Role İsmi alanı boş olamaz")]
        [Display(Name = "Role İsmi :")]
        public string Name { get; set; }
    }
}
