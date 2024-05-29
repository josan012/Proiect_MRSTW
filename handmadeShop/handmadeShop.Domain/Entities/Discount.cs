using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.Domain.Entities
{
     public class Discount
     {
          public int Id { get; set; }
          public decimal Percentage { get; set; }
          public DateTime SetTime { get; set; }
          public DateTime ExpirationTime { get; set; }
     }
}
