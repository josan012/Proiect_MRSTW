using AutoMapper;
using handmadeShop.BusinessLogic.BusinessModels;
using handmadeShop.BusinessLogic.DTO;
using handmadeShop.BusinessLogic.Infrastructure;
using handmadeShop.BusinessLogic.Interfaces;
using handmadeShop.Domain.Entities;
using handmadeShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace handmadeShop.BusinessLogic.Services
{
     public class CartService : ICartService
     {

          private IUnitOfWork DataBase { get; set; }
          public CartService(IUnitOfWork uow) { DataBase = uow; }

          public void AddToCart(int ProductId, string category)
          {
               var products = DataBase.Products.Get(ProductId, category);
               var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
               var product = mapper.Map<Product, ProductDTO>(products);

               List<Item> cart = GetCart();



               Item newItem = new Item { Product = product, Quantity = 1 };

               int index = IsInCart(cart, ProductId);
               if (index != -1)
               {
                    cart[index].Quantity++;
               }
               else
               {

                    cart.Add(newItem);
               }

               UpdateCart(cart);

          }
          private void UpdateCart(List<Item> cart)
          {
               HttpContext.Current.Session["cart"] = cart;
          }
          private int IsInCart(List<Item> cart, int ProductId)
          {
               for (int i = 0; i < cart.Count; i++)
               {
                    if (cart[i].Product.Id == ProductId)
                    {
                         return i;
                    }
               }
               return -1;
          }
          public void ClearSession()
          {

               HttpContext.Current.Session.Clear();
          }
          public void RemoveFromTheCart(int ProductId)
          {
               List<Item> cart = GetCart();
               int index = IsInCart(cart, ProductId);
               if (cart[index].Quantity > 1)
               {

                    cart[index].Quantity--;
               }
               else
               {
                    cart.RemoveAt(index);
               }
               UpdateCart(cart);
          }

          public void Dispose()
          {
               DataBase.Dispose();
          }

          public List<Item> GetCart()
          {
               if (HttpContext.Current.Session["cart"] == null)
               {
                    HttpContext.Current.Session["cart"] = new List<Item>();
               }
               return (List<Item>)HttpContext.Current.Session["cart"];
          }

          public IEnumerable<ProductDTO> GetProducts(string category)
          {
               var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
               var products = DataBase.Products.GetAll(category);
               return mapper.Map<IEnumerable<Product>, List<ProductDTO>>(products);
          }

          public ProductDTO GetProduct(int? id, string category)
          {
               if (id == null) throw new ValidationException("The ID was not found!", "");
               var product = DataBase.Products.Get(id.Value, category);
               if (product == null) throw new ValidationException("The Coffee was not found", "");

               return new ProductDTO { Id = product.Id, Name = product.Name, Price = product.Price, Category = category, PathImage = product.PathImage };
          }
          public IEnumerable<ProductDTO> RetrieveAllProducts()
          {
               var products = DataBase.Products.RetrieveAllProducts();
               var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
               var productsDTO = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
               return productsDTO;
          }
          public void SetDiscount(DiscountDTO discountDto)
          {
               var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DiscountDTO, Discount>()).CreateMapper();
               var discount = mapper.Map<DiscountDTO, Discount>(discountDto);
               var allDiscounts = DataBase.Discounts.GetAll(null).LastOrDefault();
               if (allDiscounts == null)
               {
                    DataBase.Discounts.Create(discount, null);
                    DataBase.Save();
               }
               else
               {
                    var allDisc = new Discount
                    {
                         Id = allDiscounts.Id,
                         ExpirationTime = discount.ExpirationTime,
                         Percentage = discount.Percentage,
                         SetTime = discount.SetTime,
                    };
                    DataBase.Discounts.Update(allDisc);
                    DataBase.Save();
               }


          }

          public IEnumerable<DiscountDTO> GetAllDiscounts()
          {
               var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Discount, DiscountDTO>()).CreateMapper();
               var discounts = DataBase.Discounts.GetAll(null);
               var discountsDTO = mapper.Map<IEnumerable<Discount>, List<DiscountDTO>>(discounts);
               return discountsDTO;
          }
          public decimal CalculateTotalPrice()
          {
               var cartItems = GetCart();
               var discountDTO = GetAllDiscounts().LastOrDefault();
               var deliveryDto = GetAllDeliveriesCost().LastOrDefault();
               var totalPrice = cartItems.Sum(x => x.Quantity * x.Product.Price);


               if (discountDTO != null && discountDTO.ExpirationTime > DateTime.Now)
               {
                    var discountPercentage = discountDTO.Percentage;
                    var discountAmount = (totalPrice * discountPercentage) / 100;
                    totalPrice -= discountAmount;
               }

               totalPrice += deliveryDto?.Cost ?? 0;

               return totalPrice;
          }


          public void SetDelivery(DeliveryCostDTO deliveryCostDTO)
          {
               var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DeliveryCostDTO, DeliveryCost>()).CreateMapper();
               var delivery = mapper.Map<DeliveryCostDTO, DeliveryCost>(deliveryCostDTO);
               var allDelivery = DataBase.DeliveryCosts.GetAll(null).LastOrDefault();
               if (allDelivery == null)
               {
                    DataBase.DeliveryCosts.Create(delivery, null);
                    DataBase.Save();
               }
               else
               {
                    var allDel = new DeliveryCost
                    {
                         Id = allDelivery.Id,
                         Cost = delivery.Cost,
                    };
                    DataBase.DeliveryCosts.Update(allDel);
                    DataBase.Save();
               }

          }
          public IEnumerable<DeliveryCostDTO> GetAllDeliveriesCost()
          {
               var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DeliveryCost, DeliveryCostDTO>()).CreateMapper();
               var deliveries = DataBase.DeliveryCosts.GetAll(null);
               var deliveriesDTO = mapper.Map<IEnumerable<DeliveryCost>, List<DeliveryCostDTO>>(deliveries);
               return deliveriesDTO;
          }

          public bool RemoveDiscount()
          {
               var discount = GetAllDiscounts().LastOrDefault();
               if (discount != null)
               {
                    DataBase.Discounts.Delete(discount.Id, null);
                    DataBase.Save();
                    return true;
               }
               return false;
          }
     }
}
