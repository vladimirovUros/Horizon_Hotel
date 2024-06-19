using Application.DTO.Beds;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Rooms
{
    public class CreateRoomDTO
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public string MainImage { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public List<RoomBedDTO> Beds { get; set; } = new List<RoomBedDTO>();
        public List<int> ServiceIds { get; set; } = new List<int>();
    }
}
