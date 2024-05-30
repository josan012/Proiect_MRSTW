using handmadeShop.BusinessLogic.BusinessModels;
using handmadeShop.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.BusinessLogic.Interfaces
{
     public interface IOrderService
     {
          void MakeOrder(OrderDTO orderDTO);
          ProductDTO GetProduct(int? id, string category);
          IEnumerable<ProductDTO> GetProducts(string category);
          IEnumerable<OrderDTO> GetOrders(string category);
          OrderDTO GetOrder(int id);
          IEnumerable<OrderDTO> GetOrdersByUserId(string id);
          bool DeleteOrdersByUserId(string userId);
          ProductDTO GetProductWithouCategory(int productId);
          void Dispose();
     }
}
