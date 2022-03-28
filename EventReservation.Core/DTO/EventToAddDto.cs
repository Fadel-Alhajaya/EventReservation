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

        public decimal totalprice { set; get; }

        public string cardtokenid { set; get; }

        public string Eventtype { get; set; }
   
        public DateTime Startdate { get; set; }
    
        public DateTime Enddate { get; set; }

        public string Cardname { get; set; }

        public string fullname { get; set; }

        public string Ccv { get; set; }
        public DateTime? Expirededate { get; set; }
        public string Cardnumber { get; set; }

        public string country { get; set; }

        public string city { get; set; }
    }
}
