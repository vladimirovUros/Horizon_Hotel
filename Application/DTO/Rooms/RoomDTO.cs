using Application.DTO.Beds;
using Application.DTO.Services;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Rooms
{
    public class RoomDTO : BaseDTO
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public string Description { get; set; }
        public string RoomType { get; set; }
        public List<BedDTO> Beds { get; set; } = new List<BedDTO>();
        public List<ServiceDTO> Services { get; set; } = new List<ServiceDTO>();
        public int Guests { get; set; }
        public decimal Price { get; set; }
        public string MainImage { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public List<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
    }
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime CommentedAt { get; set; }
    }
}
