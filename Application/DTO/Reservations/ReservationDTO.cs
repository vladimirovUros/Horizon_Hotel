using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Reservations
{
    public class ReservationDTO : BaseDTO
    {
        public decimal Price { get; set; }
        public string PhoneNumber { get; set; }
        public int NumberOfPersons { get; set; }
        public string FullName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string RoomName { get; set; }
        public int UserId { get; set; }
    }
}
