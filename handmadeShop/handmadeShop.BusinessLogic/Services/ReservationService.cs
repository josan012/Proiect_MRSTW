using AutoMapper;
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
            Database.ReservationsRepository.Delete(reservationId, null);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public ReservationTableDTO GetReservation(int id)
        {
            var reservationTable = Database.ReservationsRepository.Get(id, null);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ReservationTableDTO, ReservationTable>()).CreateMapper();
            var reservation = mapper.Map<ReservationTable, ReservationTableDTO>(reservationTable);
            return reservation;
        }

        public IEnumerable<ReservationTableDTO> GetReservations()
        {
            var reservations = Database.ReservationsRepository.GetAll(null);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ReservationTableDTO, ReservationTable>()).CreateMapper();
            var reservationsDTO = mapper.Map<IEnumerable<ReservationTable>, List<ReservationTableDTO>>(reservations);
            return reservationsDTO;
        }

        public IEnumerable<ReservationTableDTO> GetReservationsByUserId(string userId)
        {
            var reservations = Database.ReservationsRepository.GetByUserId(userId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ReservationTableDTO, ReservationTable>()).CreateMapper();
            List<ReservationTableDTO> reservationsDTO = new List<ReservationTableDTO>();
            foreach (var reservation in reservations)
            {
                var reservationDto = new ReservationTableDTO
                {
                    ReservationDate = reservation.ReservationDate,
                    ReservationTime = reservation.ReservationTime,
                    PhoneNumber = reservation.PhoneNumber,
                    Message = reservation.Message,
                    ApplicationUser = reservation.ApplicationUser,
                    Id = reservation.Id,
                    FirstName = reservation.FirstName,
                    LastName = reservation.LastName,
                };
                reservationsDTO.Add(reservationDto);
            }
            return reservationsDTO;
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
