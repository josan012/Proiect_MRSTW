using handmadeShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.BusinessLogic.BusinessModels
{
     public class OrderDTO
     {
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

          public ICollection<OrderItem> Items { get; set; }

          public OrderDTO()
          {
               Items = new List<OrderItem>();

          }
          public string ApplicationUserId { get; set; }
     }
}
