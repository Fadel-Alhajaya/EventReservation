using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.DTO
{
  public class EventResultToDto
    {

        public int Eventid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
        public int? Capacity { get; set; }

        public string Eventtype { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }

        public string Status { get; set; }

        public int NoPerson { get; set; }

        public string cardtokenid { set; get; }


       

        public string country { get; set; }

        public string city { get; set; }

        public string Cardname { get; set; }

        public string cardtype { get; set; }

        public string Ccv { get; set; }
        public long? ExpMonth { get; set; }
        public long? ExpYear { get; set; }

        public string Cardnumber { get; set; }

        public decimal totalprice { set; get; }

        public float? reservationprice { get; set; }





    }
}
