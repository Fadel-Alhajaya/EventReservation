using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EventReservation.Core.Data
{
   public  class Review
    {
        [Key]
        public int Id { get; set; }
        public int Rate { get; set; }

        public int WebsiteId { get; set; }
        [ForeignKey("Websiteid")]
        public virtual Website Website { get; set; }
    }
}
