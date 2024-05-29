using handmadeShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.Domain.Interfaces
{
     public interface IRepository<T> where T : class
     {
          IEnumerable<T> GetAll(string category);
          T Get(int id, string category);
          IEnumerable<T> Find(Func<T, bool> predicate);//permite efectuarea căutări mai complexe
          void Create(T item, string category);
          void Delete(int id, string category);
          T GetProduct(int id);
          IEnumerable<T> GetOrdersByUserId(string userId);
          IEnumerable<Order> GetAllOrdersWithUsers(string userId);
          IEnumerable<Product> RetrieveAllProducts();
          IEnumerable<T> GetByUserId(string userId);
          void Update(T item);
     }
}
