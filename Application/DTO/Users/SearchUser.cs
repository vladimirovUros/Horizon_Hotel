using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Users
{
    public class SearchUser : PagedSearch
    {
        public string Keyword { get; set; }
        public int? MinimumNumberOfReservations { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateOfBirthFrom { get; set; }
        public DateTime? DateOfBirthTo { get; set; }
    }
}
