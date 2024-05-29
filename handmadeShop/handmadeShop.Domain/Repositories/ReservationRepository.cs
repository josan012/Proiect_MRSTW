using handmadeShop.Domain.Entities;
using handmadeShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.Domain.Repositories
{
     public class ReservationRepository : IRepository<ReservationTable>
     {
          private EF.AppContext db;
          public ReservationRepository(EF.AppContext db) { this.db = db; }
          public void Create(ReservationTable item, string category)
          {
               db.Reservations.Add(item);
          }

          public void Delete(int id, string category)
          {
               var reservation = db.Reservations.Find(id);
               db.Reservations.Remove(reservation);
          }

          public IEnumerable<ReservationTable> Find(Func<ReservationTable, bool> predicate)
          {
               throw new NotImplementedException();
          }

          public ReservationTable Get(int id, string category)
          {
               var reservation = db.Reservations.Find(id);
               return reservation;
          }

          public IEnumerable<ReservationTable> GetAll(string category)
          {
               var reservations = db.Reservations.ToList();
               return reservations;
          }

          public IEnumerable<Order> GetAllOrdersWithUsers(string userId)
          {
               throw new NotImplementedException();
          }

          public IEnumerable<ReservationTable> GetOrdersByUserId(string userId)
          {
               throw new NotImplementedException();
          }

          public ReservationTable GetProduct(int id)
          {
               throw new NotImplementedException();
          }

          public IEnumerable<Product> RetrieveAllProducts()
          {
               throw new NotImplementedException();
          }

          public void Update(ReservationTable item)
          {
               throw new NotImplementedException();
          }
          public IEnumerable<ReservationTable> GetByUserId(string userId)
          {
               List<ReservationTable> reservations = new List<ReservationTable>();
               foreach (var reservation in db.Reservations)
               {
                    if (reservation.ApplicationUserId == userId)
                    {
                         reservations.Add(reservation);
                    }
               }
               return reservations;
          }
     }
}
