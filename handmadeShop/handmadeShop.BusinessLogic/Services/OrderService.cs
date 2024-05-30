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
     public class OrderService : IOrderService
     {
          IUnitOfWork DataBase { get; set; }
          public OrderService(IUnitOfWork uow) { DataBase = uow; }
          public void MakeOrder(OrderDTO orderDTO)
          {
               var cartItems = (List<Item>)HttpContext.Current.Session["cart"];



               Order order = new Order
               {
                    FirstName = orderDTO.FirstName,
                    LastName = orderDTO.LastName,
                    Appartment = orderDTO.Appartment,
                    StreetAddress = orderDTO.StreetAddress,
                    Phone = orderDTO.Phone,
                    City = orderDTO.City,
                    Country = orderDTO.Country,
                    PostCode = orderDTO.PostCode,
                    Email = orderDTO.Email,
                    TotalSumToPay = orderDTO.TotalSumToPay,
                    BuyingTime = DateTime.Now,
                    ApplicationUserId = orderDTO.ApplicationUserId
               };

               foreach (var cartItem in cartItems)
               {
                    var orderItem = new OrderItem
                    {
                         ProductId = cartItem.Product.Id, // Associate existing Product with OrderItem
                         Order = order,
                         Quantity = cartItem.Quantity,

                    };

                    order.Items.Add(orderItem);
               }
               DataBase.Orders.Create(order, "");
               DataBase.Save();
          }
          public ProductDTO GetProductWithouCategory(int productId)
          {

               var product = DataBase.Products.GetProduct(productId);
               if (product == null) throw new ValidationException($"The {product.Name} was not found", "");
               return new ProductDTO { Id = product.Id, Name = product.Name, Price = product.Price, PathImage = product.PathImage };
          }
          public ProductDTO GetProduct(int? id, string category)
          {
               if (id == null) throw new ValidationException("The ID was not found!", "");
               var product = DataBase.Products.Get(id.Value, category);
               if (product == null) throw new ValidationException($"The {product.Name} was not found", "");

               return new ProductDTO { Id = product.Id, Name = product.Name, Price = product.Price, Category = category };
          }

          public decimal CalculateTotalSum(OrderDTO orderDTO, string category)
          {
               decimal totalSum = 0;
               var products = DataBase.Products.GetAll(category);
               foreach (var productId in products)
               {
                    ProductDTO product = GetProduct(orderDTO.Id, category);
                    totalSum += product.Price;
               }

               return totalSum;
          }

          public bool DeleteOrdersByUserId(string userId)
          {
               try
               {
                    var orders = DataBase.Orders.GetOrdersByUserId(userId);
                    if (orders != null)
                    {
                         foreach (var order in orders)
                         {
                              DataBase.Orders.Delete(order.Id, null);
                         }
                         return true;
                    }
                    return false;
               }
               catch (Exception ex)
               {
                    throw new Exception(ex.Message);

               }
          }
          public void Dispose()
          {
               DataBase.Dispose();
          }

          public IEnumerable<OrderDTO> GetOrders(string category)
          {
               var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>()).CreateMapper();
               return mapper.Map<IEnumerable<Order>, List<OrderDTO>>(DataBase.Orders.GetAll(category));
          }

          public IEnumerable<ProductDTO> GetProducts(string category)
          {
               var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
               return mapper.Map<IEnumerable<Product>, List<ProductDTO>>(DataBase.Products.GetAll(category));
          }

          public OrderDTO GetOrder(int id)
          {
               var order = DataBase.Orders.Get(id, "");
               var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>()).CreateMapper();
               return mapper.Map<Order, OrderDTO>(order);
          }

          public IEnumerable<OrderDTO> GetOrdersByUserId(string id)
          {
               var orders = DataBase.Orders.GetOrdersByUserId(id);
               var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>()).CreateMapper();
               return mapper.Map<IEnumerable<Order>, List<OrderDTO>>(orders);
          }
     }
}
