using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Messages
{
    public class MessageDTO : BaseDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string TextMessage { get; set; }
        public DateTime DateOfSend { get; set; }
    }
}
