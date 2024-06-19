using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Comments
{
    public class CreateCommentDTO
    {
        public string Text { get; set; }
        public int RoomId { get; set; }
        public int AuthorId { get; set; }
    }
}
