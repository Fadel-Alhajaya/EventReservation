using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.DTO
{
   public class UserToUpdateDto
    {
        public IFormFile ImageFile;

        public int Userid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string Email { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Image { get; set; }
        public string PublicId { get; set; }
    }
}
