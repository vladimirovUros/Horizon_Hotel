using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class RoomImage
    {
        public int RoomId { get; set; }
        public int ImageId { get; set; }

        public virtual Room Room { get; set; }
        public virtual Image Image { get; set; }
    }
}
