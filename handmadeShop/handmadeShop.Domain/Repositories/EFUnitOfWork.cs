using handmadeShop.Domain.Entities;
using handmadeShop.Domain.Identity;
using handmadeShop.Domain.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.Domain.Repositories
{
     public class EFUnitOfWork : IUnitOfWork
     {
          private EF.AppContext db;
          private ProductRepository productRepository;
          private ReservationRepository reservationRepository;
          private OrderRepository orderRepository;
          private ApplicationUserManager userManager;
          private ApplicationRoleManager roleManager;
          private DiscountRepository discountRepository;

          private DeliveryCostRepository deliveryCostRepository;



          private IClientManager clientManager;

          public EFUnitOfWork()
          {
               this.db = new EF.AppContext();
               userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
               roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
               clientManager = new ClientManager(db);
          }

          public IRepository<Product> Products
          {
               get
               {
                    if (productRepository == null) return new ProductRepository(db);
                    return productRepository;
               }
          }
          public IRepository<Order> Orders
          {
               get
               {
                    if (orderRepository == null) return new OrderRepository(db);
                    return orderRepository;
               }
          }
          public ApplicationUserManager UserManager => userManager;

          public IClientManager ClientManager => clientManager;

          public ApplicationRoleManager RoleManager => roleManager;

          public IRepository<ReservationTable> ReservationsRepository
          {
               get
               {
                    if (reservationRepository == null) return new ReservationRepository(db);
                    return reservationRepository;
               }
          }

          public IRepository<Discount> Discounts
          {
               get
               {
                    if (discountRepository == null) return new DiscountRepository(db);
                    return discountRepository;
               }
          }

          public IRepository<DeliveryCost> DeliveryCosts
          {
               get
               {
                    if (deliveryCostRepository == null) return new DeliveryCostRepository(db);
                    return deliveryCostRepository;
               }
          }

          public void Save()
          {
               db.SaveChanges();
          }
          public void Dispose()
          {
               Dispose(true);
               GC.SuppressFinalize(this);
          }
          private bool disposed = false;

          public virtual void Dispose(bool disposing)
          {
               if (!this.disposed)
               {
                    if (disposing)
                    {

                         userManager.Dispose();
                         roleManager.Dispose();
                         clientManager.Dispose();
                    }
                    this.disposed = true;
               }
          }

          public async Task SaveAsync()
          {
               await db.SaveChangesAsync();
          }
     }
}
