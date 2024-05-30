using handmadeShop.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.BusinessLogic.Interfaces
{
    public interface IReservationService
    {
        void MakeReservation(ReservationTableDTO reservationDTO);
        ReservationTableDTO GetReservation(int id);
        IEnumerable<ReservationTableDTO> GetReservations();
        IEnumerable<ReservationTableDTO> GetReservationsByUserId(string userId);
        void DeleteReservation(int reservationId);
        void Dispose();
    }
}
