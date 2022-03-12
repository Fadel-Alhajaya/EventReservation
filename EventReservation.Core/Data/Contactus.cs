using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventReservation.Core.Data
{
    public  class Contactus
    {
        [Key]
        public int Contactusid { get; set; }
        public string Personalname { get; set; }
        public string Phonenumber { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        [ForeignKey("Websiteid")]
        public int? Websiteid { get; set; }

        public virtual Website Website { get; set; }
    }
}
