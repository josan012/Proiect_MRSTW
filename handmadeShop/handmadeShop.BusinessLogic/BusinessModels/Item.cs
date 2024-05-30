using handmadeShop.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.BusinessLogic.BusinessModels
{
    public class Item
    {
        public ProductDTO Product { get; set; }
        public int Quantity { get; set; }

    }
}
