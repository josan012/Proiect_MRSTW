using handmadeShop.Domain.Entities;
using handmadeShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.Domain.Repositories
{
    public class DeliveryCostRepository : IRepository<DeliveryCost>
    {
        public void Create(DeliveryCost item, string category)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id, string category)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DeliveryCost> Find(Func<DeliveryCost, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public DeliveryCost Get(int id, string category)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DeliveryCost> GetAll(string category)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
