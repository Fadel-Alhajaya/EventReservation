using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.DTO
{
    public class UsertoResultDto
    {
        public string Firstname { get; set; }
        public int Userid { get; set; }
        public string Lastname { get; set; }
        
        public string PublicId { get; set; }

        public string Email { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Image { get; set; }

        public string Position { get; set; }

        public string Username { get; set; }
    }
}
