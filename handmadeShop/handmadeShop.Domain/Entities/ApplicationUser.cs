using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<ReservationTable> Reservations { get; set; }

        public ApplicationUser()
        {
            Orders = new List<Order>();
            Reservations = new List<ReservationTable>();
        }

    }
}
