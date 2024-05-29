using handmadeShop.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.Domain.EF
{
    public class AppContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<DeliveryCost> DeliveryCosts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ReservationTable> Reservations { get; set; }

        public AppContext() : base() { }
        public static AppContext Create()
        {
            return new AppContext();
        }

    }
}
