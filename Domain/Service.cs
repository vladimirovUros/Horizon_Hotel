using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Service : NamedEntity
    {
        public int IconId { get; set; }

        public virtual Image Icon { get; set; }
        public virtual ICollection<RoomService> RoomServices { get; set; } = new HashSet<RoomService>();
    }
}
