using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Beds
{
    public class SearchBed : PagedSearch
    {
        public string Keyword { get; set; }
    }
}
