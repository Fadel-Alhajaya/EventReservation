using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventReservation.Core.Data
{
    public  class Image
    {
        [Key]
        public int Imageid { get; set; }
        public string Imageurl { get; set; }
        public string Description { get; set; }
        public string PublicId { get; set; }
      
        public int? Hallid { get; set; }
        [ForeignKey("Hallid")]
        public virtual Hall Hall { get; set; }
    }
}
