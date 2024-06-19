using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Services
{
    public class SearchService : PagedSearch
    {
        public string Keyword { get; set; }
    }
}
