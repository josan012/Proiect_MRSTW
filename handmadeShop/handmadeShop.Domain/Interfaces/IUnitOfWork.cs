using handmadeShop.Domain.Entities;
using handmadeShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.Domain.Interfaces
{
     public interface IUnitOfWork : IDisposable
     {
          IRepository<DeliveryCost> DeliveryCosts { get; }
          IRepository<Discount> Discounts { get; }
          IRepository<ReservationTable> ReservationsRepository { get; }
          IRepository<Product> Products { get; }
          IRepository<Order> Orders { get; }
          ApplicationUserManager UserManager { get; }
          IClientManager ClientManager { get; }
          ApplicationRoleManager RoleManager { get; }

          Task SaveAsync();
          void Save();
     }
}
