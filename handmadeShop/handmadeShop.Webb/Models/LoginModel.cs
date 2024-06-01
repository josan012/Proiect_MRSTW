using System.ComponentModel.DataAnnotations;

namespace handmadeShop.Web.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}