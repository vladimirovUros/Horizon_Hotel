using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Price : Entity
    {
        public decimal RoomPrice { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        //public bool IsActive { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
    }
}
