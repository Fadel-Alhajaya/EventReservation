using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventReservation.Core.DTO
{
    public class RoleToUpdateDTO
    {
      
       
        [Required]
        public int Roleid { get; set; }
        [Required]
        public string Position { get; set; }


    }
}
