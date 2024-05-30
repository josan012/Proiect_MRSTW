using handmadeShop.Domain.Entities;
using handmadeShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace handmadeShop.Domain.Repositories
{
    public class DeliveryCostRepository : IRepository<DeliveryCost>
     {
          private EF.AppContext db;
          public DeliveryCostRepository(EF.AppContext db)
          {
               this.db = db;
          }

          public void Create(DeliveryCost item, string category)
          {
               db.DeliveryCosts.Add(item);
          }

          public void Delete(int id, string category)
          {
               var deliveryCost = db.DeliveryCosts.Find(id);
               db.DeliveryCosts.Remove(deliveryCost);
          }

          public IEnumerable<DeliveryCost> Find(Func<DeliveryCost, bool> predicate)
          {
               throw new NotImplementedException();
          }

          public DeliveryCost Get(int id, string category)
          {
               return db.DeliveryCosts.Find(id);
          }

          public IEnumerable<DeliveryCost> GetAll(string category)
          {
               return db.DeliveryCosts.ToList();
          }

          public IEnumerable<Order> GetAllOrdersWithUsers(string userId)
          {
               throw new NotImplementedException();
          }

          public IEnumerable<DeliveryCost> GetByUserId(string userId)
          {
               throw new NotImplementedException();
          }

          public IEnumerable<DeliveryCost> GetOrdersByUserId(string userId)
          {
               throw new NotImplementedException();
          }

          public DeliveryCost GetProduct(int id)
          {
               throw new NotImplementedException();
          }

          public IEnumerable<Product> RetrieveAllProducts()
          {
               throw new NotImplementedException();
          }

          public void Update(DeliveryCost item)
          {
               var delivery = db.DeliveryCosts.Find(item.Id);
               if (delivery != null)
               {
                    delivery.Cost = item.Cost;
               }
          }
     }
}
