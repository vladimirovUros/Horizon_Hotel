using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Types
{
    public class SearchType : PagedSearch
    {
        public string Keyword { get; set; }
        public int? MinimumCapacity { get; set; }
        public int? MaximumCapacity { get; set; }
    }
}
