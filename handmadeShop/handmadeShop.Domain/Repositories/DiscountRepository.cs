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
        public void Create(Discount item, string category)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id, string category)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Discount> Find(Func<Discount, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Discount Get(int id, string category)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Discount> GetAll(string category)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
