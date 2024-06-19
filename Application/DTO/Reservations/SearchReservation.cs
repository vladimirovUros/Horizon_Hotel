using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Reservations
{
    public class SearchReservation : PagedSearch
    {
        public string Keyword { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public bool IsActive { get; set; }
    }
}
