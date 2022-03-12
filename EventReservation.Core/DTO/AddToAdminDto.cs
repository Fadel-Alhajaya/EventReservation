
using EventReservation.Core.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventReservation.Core.DTO
{
    public class AddToAdminDto
    {
        [Required(ErrorMessage = "This field is required")]
        public string Username { get; set; }
        
        public string Password { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Email { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Image { get; set; }
        public IFormFile ImageFile { get; set; }
        public string PublicId { get; set; }
    }
}
