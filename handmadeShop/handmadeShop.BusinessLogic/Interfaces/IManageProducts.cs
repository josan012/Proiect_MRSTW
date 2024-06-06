using handmadeShop.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.BusinessLogic.Interfaces
{
    public interface IManageProducts
    {
        void UpdateProduct(ProductDTO productDTO);
        void AddProduct(ProductDTO productDTO);
        void DeleteProduct(int productId);
        void Dispose();
    }
}
