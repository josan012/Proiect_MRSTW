using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace handmadeShop.Web.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public ICollection<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();

        public virtual ICollection<ReservationViewModel> Reservations { get; set; }

        public UserModel()
        {
            Reservations = new List<ReservationViewModel>();
        }
    }
}