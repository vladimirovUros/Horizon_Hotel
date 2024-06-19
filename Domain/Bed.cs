using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Bed : NamedEntity
    {
        public virtual ICollection<RoomBed> RoomBeds { get; set; } = new HashSet<RoomBed>();
    }
}
