using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Comments
{
    public class CommentDTO : BaseDTO
    {
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
