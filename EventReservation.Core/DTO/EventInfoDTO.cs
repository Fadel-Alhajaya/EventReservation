using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.DTO
{
    public class EventInfoDTO
    {
        public int Eventid { get; set; }
        public string eventtype { set; get; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public int NoPerson { get; set; }
        public string status { set; get; }
        public string name { set; get; }
        public string usage { set; get; }
        public decimal rate { set; get; }
        public decimal reservationprice { set; get; }

        public decimal totalprice { set; get; }

        public string cardtokenid { set; get; }











    }
}
