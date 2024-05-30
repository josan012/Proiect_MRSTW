using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.BusinessLogic.BusinessModels
{
     public class DiscountDTO
     {
          public int Id { get; set; }
          public decimal Percentage { get; set; }
          public DateTime SetTime { get; set; }
          public DateTime ExpirationTime { get; set; }
     }
}
