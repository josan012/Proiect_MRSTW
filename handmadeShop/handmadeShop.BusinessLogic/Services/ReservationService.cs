using handmadeShop.BusinessLogic.DTO;
using handmadeShop.BusinessLogic.Interfaces;
using handmadeShop.Domain.Entities;
using handmadeShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.BusinessLogic.Services
{
    public class ReservationService : IReservationService
    {
        private IUnitOfWork Database;
        public ReservationService(IUnitOfWork database)
        {
            Database = database;
        }
        public void DeleteReservation(int reservationId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public ReservationTableDTO GetReservation(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReservationTableDTO> GetReservations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReservationTableDTO> GetReservationsByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void MakeReservation(ReservationTableDTO reservationDTO)
        {
            ReservationTable reservationTable = new ReservationTable
            {
                ReservationDate = reservationDTO.ReservationDate,
                ReservationTime = reservationDTO.ReservationTime,
                Id = reservationDTO.Id,
                FirstName = reservationDTO.FirstName,
                LastName = reservationDTO.LastName,
                Message = reservationDTO.Message,
                PhoneNumber = reservationDTO.PhoneNumber,
                ApplicationUser = reservationDTO.ApplicationUser,
                ApplicationUserId = reservationDTO.ApplicationUserId,
            };
            Database.ReservationsRepository.Create(reservationTable, null);
            Database.Save();
        }
    }
}
