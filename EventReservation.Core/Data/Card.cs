using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventReservation.Core.Data
{
    public  class Card
    {
        [Key]
        public int Cardid { get; set; }
        public string Ccv { get; set; }
        public DateTime? Expirededate { get; set; }
        public float? Balance { get; set; }
        public int? Cardnumber { get; set; }
        public string Cardtype { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
