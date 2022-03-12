using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.DTO.WebsiteDto
{
   public class AddToWebsiteDto
    {
       
        public string Websitename { get; set; }
        public string Logopath { get; set; }
        public string Backgroundimg { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Worktime { get; set; }
        public float? Wallet { get; set; }
        public int? Adminid { get; set; }



    }
}
