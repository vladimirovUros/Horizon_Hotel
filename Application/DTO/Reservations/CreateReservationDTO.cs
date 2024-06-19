using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Reservations
{
    public class CreateReservationDTO
    {
        public string PhoneNumber { get; set; }
        public int NumberOfPersons { get; set; }
        public string FullName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int RoomId { get; set; }
    }
}
