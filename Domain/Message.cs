using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Message
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string TextMessage { get; set; }
        public DateTime DateOfSend { get; set; }
    }
}
