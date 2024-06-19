using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.AuditLogs
{
    public class AuditLogDTO : BaseDTO
    {
        public DateTime ExecutedAt { get; set; }
        public string UseCaseName { get; set; }
        public string Actor { get; set; }
        public string Data { get; set; }

    }
}
