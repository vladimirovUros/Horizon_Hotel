using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Reservations
{
    public class UpdateReservationDTO : CreateReservationDTO
    {
        public int ReservationId { get; set; }
    }
}
