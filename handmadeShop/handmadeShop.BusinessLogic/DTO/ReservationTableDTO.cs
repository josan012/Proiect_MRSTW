using handmadeShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.BusinessLogic.DTO
{
    public class ReservationTableDTO
    {
        public int Id { get; set; }

        public DateTime ReservationDate { get; set; }
        [Display(Name = "Time")]
        public string ReservationTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }

        //Relatia cu utilizatorul
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
