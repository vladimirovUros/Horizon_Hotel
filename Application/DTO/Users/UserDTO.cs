using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Users
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public int NumberOfReservations { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
