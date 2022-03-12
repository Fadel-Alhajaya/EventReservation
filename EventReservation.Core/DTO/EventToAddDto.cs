using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventReservation.Core.DTO
{
   public  class EventToAddDto
    {
        [Required]
        public int HallId { get; set; }
        [Required]
        public int UserId { get; set; }
        
        public int NoPerson { get; set; }
        public string Eventtype { get; set; }
        [Required]
        public DateTime Startdate { get; set; }
        [Required]
        public DateTime Enddate { get; set; }
    }
}
