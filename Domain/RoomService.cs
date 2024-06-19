using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class RoomService
    {
        public int RoomId { get; set; }
        public int ServiceId { get; set; }
        public Room Room { get; set; }
        public Service Service { get; set; }
    }
}
