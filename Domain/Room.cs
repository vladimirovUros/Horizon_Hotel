using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Room : NamedEntity
    {
        public int Size { get; set; }
        public string Description { get; set; }
        public int MainImageId { get; set; }
        public int TypeId { get; set; }

        public virtual Type Type { get; set; }
        public virtual ICollection<RoomBed> RoomBeds { get; set; } = new HashSet<RoomBed>();
        public virtual ICollection<RoomService> RoomServices { get; set; } = new HashSet<RoomService>();
        public virtual ICollection<Price> Prices { get; set; } = new HashSet<Price>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
        public virtual ICollection<OccupiedRoom> OccupiedRooms { get; set; } = new HashSet<OccupiedRoom>();
        public virtual ICollection<RoomImage> RoomImages { get; set; } = new HashSet<RoomImage>();
        public virtual Image MainImage { get; set; }
    }
}
