using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Comment : Entity
    {
        public string Text { get; set; }
        public int AuthorId { get; set; }
        public int RoomId { get; set; }

        public virtual User Author { get; set; }
        public virtual Room Room { get; set; }
    }
}
