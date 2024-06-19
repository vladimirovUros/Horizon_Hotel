using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Type : NamedEntity
    {
        public int Capacity { get; set; }

        public virtual ICollection<Room> Rooms { get; set; } = new HashSet<Room>();
    }
}
