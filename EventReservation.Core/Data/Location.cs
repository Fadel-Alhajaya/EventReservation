using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventReservation.Core.Data
{
    public  class Location
    {
     
        public int Locationid { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

        public virtual ICollection<Hall> HallF { get; set; }
    }
}
