using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Comments
{
    public class SearchComment : PagedSearch
    {
        public string Keyword { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
