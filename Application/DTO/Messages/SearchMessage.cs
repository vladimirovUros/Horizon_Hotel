using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Messages
{
    public class SearchMessage : PagedSearch
    {
        public string Keyword { get; set; }
        public DateTime? DateOfSend { get; set; }
    }
}
