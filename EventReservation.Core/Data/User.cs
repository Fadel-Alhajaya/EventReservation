using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventReservation.Core.Data
{
    public class User
    {
        [Key]
        public int Userid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Email { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Image { get; set; }
        public int? Cardid { get; set; }
        public int? Loginid { get; set; }
        public int? Roleid { get; set; }
        [ForeignKey("Cardid")]
        public virtual Card Card { get; set; }
        [ForeignKey("Loginid")]
        public virtual Login Login { get; set; }
        [ForeignKey("Roleid")]
        public virtual Role Role { get; set; }
        public virtual ICollection<Website> Website { get; set; }
        public string PublicId { get; set; }
    }
}
