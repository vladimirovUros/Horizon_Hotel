using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Reservation : Entity
    {
        public decimal Price { get; set; }
        public string PhoneNumber { get; set; }
        public int NoOfPeople { get; set; }
        public string FullName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }

        public virtual Room Room { get; set; }
        public virtual User User { get; set; }
    }
}
