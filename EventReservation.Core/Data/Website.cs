using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventReservation.Core.Data
{
    public class Website
    {
      
        [Key]
        public int Websiteid { get; set; }
        public string Websitename { get; set; }
        public string Logopath { get; set; }
        public string Backgroundimg { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Worktime { get; set; }
        public float? Wallet { get; set; }
        public int? Adminid { get; set; }
        [ForeignKey("Adminid")]
        public virtual User Admin { get; set; }
        public virtual ICollection<Aboutus> Aboutus { get; set; }
        public virtual ICollection<Contactus> Contactus { get; set; }
        public virtual ICollection<Testimonial> Testimonial { get; set; }
    }
}
