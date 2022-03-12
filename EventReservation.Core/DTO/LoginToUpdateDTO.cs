using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventReservation.Core.DTO
{
   public class LoginToUpdateDTO
    {   [Required]
        public int Loginid { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Passwordhash { get; set; }
        [Required]
        public string Passwordsalt { get; set; }


    }
}
