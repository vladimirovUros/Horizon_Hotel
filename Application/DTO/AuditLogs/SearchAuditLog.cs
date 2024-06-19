using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.AuditLogs
{
    public class SearchAuditLog : PagedSearch
    {
        public string UseCaseName { get; set; }
        public string Actor { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
