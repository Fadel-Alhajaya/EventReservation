using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventReservation.Core.Data
{
    public  class Hall
    {
     
        [Key]
        public int Hallid { get; set; }
        public string Name { get; set; }
        public int? Capacity { get; set; }
        public int? Waiters { get; set; }
        public int? Sale { get; set; }
        public float? Resrvitionprice { get; set; }  
        public int? Locationid { get; set; }
        [ForeignKey("Locationid")]
        public virtual Location Location { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<Image> Image { get; set; }
    }
}
