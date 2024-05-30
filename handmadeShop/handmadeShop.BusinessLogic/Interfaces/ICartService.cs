using handmadeShop.BusinessLogic.BusinessModels;
using handmadeShop.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.BusinessLogic.Interfaces
{
     public interface ICartService
     {
          void AddToCart(int ProductId, string category);
          void RemoveFromTheCart(int ProductId);
          List<Item> GetCart();
          void ClearSession();
          ProductDTO GetProduct(int? id, string category);
          IEnumerable<ProductDTO> GetProducts(string category);
          IEnumerable<ProductDTO> RetrieveAllProducts();
          void SetDiscount(DiscountDTO discountDto);
          IEnumerable<DiscountDTO> GetAllDiscounts();
          decimal CalculateTotalPrice();

          IEnumerable<DeliveryCostDTO> GetAllDeliveriesCost();
          void SetDelivery(DeliveryCostDTO deliveryCostDTO);
          bool RemoveDiscount();
          void Dispose();

     }
}
