using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventReservation.Core.Data
{
    public  class Event
    {
        [Key]
        public int Eventid { get; set; }
        public string Eventtype { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }

        public string Status { get; set; }

        public int NoPerson { get; set; }

        public string cardtokenid { set; get; }
        public int? Hallid { get; set; }

        [ForeignKey("Hallid")]
        public virtual Hall Hall { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }




    }
}
