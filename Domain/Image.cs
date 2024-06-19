using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Image : Entity
    {
        public string Path { get; set; }
        public virtual ICollection<RoomImage> RoomImages { get; set; } = new HashSet<RoomImage>();
        public virtual ICollection<Service> Services { get; set; } = new HashSet<Service>();
        public virtual ICollection<Room> Rooms { get; set; } = new HashSet<Room>();
    }
}
