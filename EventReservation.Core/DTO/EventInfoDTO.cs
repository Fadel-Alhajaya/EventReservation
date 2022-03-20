using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.DTO
{
    public class EventInfoDTO
    {

        //public decimal cntevent { set; get; }
        
        public string eventtype { set; get; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string status { set; get; }
        public string name { set; get; }
        public string usage { set; get; }
        public decimal rate { set; get; }
        public decimal RESRVITIONPRICE { set; get; }
        




    }
}
