using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.Domain.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string Appartment { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal TotalSumToPay { get; set; }
        public DateTime BuyingTime { get; set; }

        //Relatia cu utilizatorul
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        //Relatia cu produsul cumparat
        public ICollection<OrderItem> Items { get; set; }

        public Order()
        {
            Items = new List<OrderItem>();
        }



    }
}
