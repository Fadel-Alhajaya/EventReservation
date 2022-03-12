using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.DTO.UserDto
{
  public  class UserToResultRegisterDto
    {
        public int Loginid { get; set; }

        public int UserId { get; set; }
        public string Username { get; set; }

        public string Position { get; set; }

        public string Fullname { get; set; }
        public string Email { get; set; }



    }
}
