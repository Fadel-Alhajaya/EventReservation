using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.DTO
{
    public class LoginToDto
    {

        public int Loginid { get; set; }

        public int UserId { get; set; }
        public string Username { get; set; }

        public string Position { get; set; }

        public string Fullname { get; set; }
        public string Email { get; set; }

        public byte[] Passwordhash { get; set; }
        public byte[] Passwordsalt { get; set; }




    }
}
