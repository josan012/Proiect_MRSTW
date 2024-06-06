using handmadeShop.Domain.Entities;
using handmadeShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.Domain.Repositories
{
     public class ProductRepository : IRepository<Product>
     {
          private EF.AppContext db;
          public ProductRepository(EF.AppContext db)
          {
               this.db = db;
          }
          public void Create(Product item, string category)
          {
               db.Products.Add(item);
          }

          public void Delete(int id, string category)
          {
               var product = db.Products.FirstOrDefault(c => c.Category == category && c.Id == id);
               if (product != null)
               {
                    db.Products.Remove(product);
               }
          }
          public void Update(Product item)
          {
               var existingProduct = db.Products.Find(item.Id);
               if (existingProduct != null)
               {
                    existingProduct.Name = item.Name;
                    existingProduct.Price = item.Price;
                    existingProduct.Category = item.Category;
                    existingProduct.PathImage = item.PathImage;

               }
          }

          public IEnumerable<Product> Find(Func<Product, bool> predicate)
          {
               return db.Products.Where(predicate);
          }

          public Product Get(int id, string category)
          {
               return db.Products.FirstOrDefault(p => p.Id == id && p.Category == category);
          }

          public IEnumerable<Product> GetAll(string category)
          {
               return db.Products.Where(c => c.Category == category).ToList();
          }
          public Product GetProduct(int id)
          {
               return db.Products.FirstOrDefault(x => x.Id == id);
          }
          public IEnumerable<Product> RetrieveAllProducts()
          {
               return db.Products.ToList();
          }
          public IEnumerable<Product> GetOrdersByUserId(string userId)
          {
               throw new NotImplementedException();
          }

          public IEnumerable<Order> GetAllOrdersWithUsers(string userId)
          {
               throw new NotImplementedException();
          }

          public IEnumerable<Product> GetByUserId(string userId)
          {
               throw new NotImplementedException();
          }
     }
}
