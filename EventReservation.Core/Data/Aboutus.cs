using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventReservation.Core.Data
{
    public  class Aboutus
    {
        [Key]
        public int Aboutusid { get; set; }
        [Required(ErrorMessage = "This Feild is required")]
        public string Description { get; set; }
        public string Imagepath { get; set; }
   
        public int Websiteid { get; set; }
        [ForeignKey("Websiteid")]
        public virtual Website Website { get; set; }
    }
}
