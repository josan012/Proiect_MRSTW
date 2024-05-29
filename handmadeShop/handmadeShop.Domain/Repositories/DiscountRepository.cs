using handmadeShop.Domain.Entities;
using handmadeShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.Domain.Repositories
{
     public class DiscountRepository : IRepository<Discount>
     {
          private EF.AppContext db;
          public DiscountRepository(EF.AppContext db)
          {
               this.db = db;
          }
          public void Create(Discount item, string category)
          {
               db.Discounts.Add(item);
          }

          public void Delete(int id, string category)
          {
               var discount = db.Discounts.Find(id);
               db.Discounts.Remove(discount);
          }

          public IEnumerable<Discount> Find(Func<Discount, bool> predicate)
          {
               throw new NotImplementedException();
          }

          public Discount Get(int id, string category)
          {
               return db.Discounts.Find(id);
          }

          public IEnumerable<Discount> GetAll(string category)
          {
               return db.Discounts.ToList();
          }

          public IEnumerable<Order> GetAllOrdersWithUsers(string userId)
          {
               throw new NotImplementedException();
          }

          public IEnumerable<Discount> GetByUserId(string userId)
          {
               throw new NotImplementedException();
          }

          public IEnumerable<Discount> GetOrdersByUserId(string userId)
          {
               throw new NotImplementedException();
          }

          public Discount GetProduct(int id)
          {
               throw new NotImplementedException();
          }

          public IEnumerable<Product> RetrieveAllProducts()
          {
               throw new NotImplementedException();
          }

          public void Update(Discount item)
          {
               Discount discount = db.Discounts.Find(item.Id);
               if (discount != null)
               {
                    discount.ExpirationTime = item.ExpirationTime;
                    discount.SetTime = item.SetTime;
                    discount.Percentage = item.Percentage;
               }
          }
     }
}
