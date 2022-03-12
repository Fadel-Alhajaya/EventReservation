using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventReservation.Core.Data
{
    public  class Event
    {
        public int Eventid { get; set; }
        public string Eventtype { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public string Status { get; set; }
        public int? Hallid { get; set; }
        [ForeignKey("Hallid")]
        public virtual Hall Hall { get; set; }
        public int? Userid { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
      
    }
}
