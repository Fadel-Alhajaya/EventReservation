using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventReservation.Core.DTO
{
    public class ImageToAddDto
    {
        
        public string ImageUrl { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }
        public string Description { get; set; }
        [Required]
        public int Hallid { get; set; }

        public string Publicid { get; set; }

    }
}
