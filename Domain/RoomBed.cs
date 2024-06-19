using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class RoomBed
    {
        public int RoomId { get; set; }
        public int BedId { get; set; }
        public int Quantity { get; set; }

        public virtual Room Room { get; set; }
        public virtual Bed Bed { get; set; }
    }
}
