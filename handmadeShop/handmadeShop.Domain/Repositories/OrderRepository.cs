using handmadeShop.Domain.Entities;
using handmadeShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.Domain.Repositories
{
     public class OrderRepository : IRepository<Order>
     {
          private EF.AppContext db;

          public OrderRepository(EF.AppContext _context)
          {
               db = _context;
          }

          public void Create(Order item, string category)
          {
               db.Orders.Add(item);
          }

          public void Delete(int id, string category)
          {
               Order order = db.Orders.Find(id);
               if (order != null)
                    db.Orders.Remove(order);
          }
          
          public IEnumerable<Order> Find(Func<Order, bool> predicate)
          {
               return db.Orders.Where(predicate).ToList();
          }

          public Order Get(int id, string category)
          {
               return db.Orders
               .Include(o => o.Items.Select(i => i.Product)) // Include the related items and their associated product
               .SingleOrDefault(o => o.Id == id);
          }
          public IEnumerable<Order> GetAllOrdersWithUsers(string userId)
          {
               return db.Orders
                    .Where(o => o.ApplicationUserId == userId)
                    .Include(o => o.ApplicationUser).Include(o => o.Items)
                    .ToList();
          }
          public IEnumerable<Order> GetAll(string category)
          {
               return db.Orders.Include(o => o.ApplicationUser).Include(o => o.Items.Select(i => i.Product)).ToList();
          }

          public IEnumerable<Order> GetOrdersByUserId(string userId)
          {
               List<Order> orders = new List<Order>();
               foreach (var order in db.Orders.Include(p => p.Items.Select(i => i.Product)))
               {
                    if (order.ApplicationUserId == userId)
                    {
                         orders.Add(order);
                    }
               }
               return orders;
          }

          public Order GetProduct(int id)
          {
               throw new NotImplementedException();
          }

          public IEnumerable<Product> RetrieveAllProducts()
          {
               throw new NotImplementedException();
          }

          public void Update(Order item)
          {
               throw new NotImplementedException();
          }

          public IEnumerable<Order> GetByUserId(string userId)
          {
               throw new NotImplementedException();
          }
     }
}
