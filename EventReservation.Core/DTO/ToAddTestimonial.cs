using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventReservation.Core.DTO
{
   public class ToAddTestimonial
    {
        public int Testimonialid { get; set; }
        [Required]
        public string Personalname { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }
        public string Feedback { get; set; }
        public string Status { get; set; }
        [Required]
        public int Websiteid { get; set; }

        public string Publicid { get; set; }
    }
}
