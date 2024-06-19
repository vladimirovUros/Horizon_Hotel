using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Reservations
{
    public class CheckReservationDTO
    {
        public int RoomId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}
