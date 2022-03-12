using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventReservation.Core.DTO
{
   public  class ImageToUpdateDto
    {
        public int  ImageId { get; set; }
        [Required]
        public string Imageurl { get; set; }
        public string Description { get; set; }
        [Required]
        public int Hallid { get; set; }

        public string Publicid { get; set; }
    }
}
