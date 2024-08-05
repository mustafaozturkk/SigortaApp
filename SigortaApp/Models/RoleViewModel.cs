using System.ComponentModel.DataAnnotations;

namespace SigortaApp.Web.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage ="Lütfen rol Adı giriniz")]
        public string Name { get; set; }
    }
}
