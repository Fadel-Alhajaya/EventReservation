using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.DTO
{
    public  class CardToDto
    {

        public string Cardname { get; set; }

        public string fullname { get; set; }

        public string Ccv { get; set; }
        public DateTime? Expirededate { get; set; }
        public string Cardnumber { get; set; }

        public string country { get; set; }

        public string city { get; set; }

        public string email { get; set; }



    }
}
