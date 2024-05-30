using handmadeShop.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.BusinessLogic.Interfaces
{
     public interface IEditProductService
     {
          void UpdateProduct(ProductDTO productDto);
          void Dispose();
     }
}
