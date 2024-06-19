using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Rooms
{
    public class SearchRoom : PagedSearch
    {
        public string Keyword { get; set; }
        public int? Size { get; set; }
        public int? TypeId { get; set; }
        public List<int> BedsId { get; set; } = new List<int>();
        public List<int> ServicesId { get; set; } = new List<int>();
        public int? Guests { get; set; }  //front - broj dece plus broj odraslih
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
