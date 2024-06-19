using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class OccupiedRoom : Entity
    {
        public DateTime Date { get; set; }
        public int RoomId { get; set; }

        public virtual Room Room { get; set; }
    }
}
