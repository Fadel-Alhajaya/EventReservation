using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventReservation.Core.DTO
{
    public class CardToUpdateDTO
    {
        public decimal Cardid { get; set; }
        [Required]
        public string Ccv { get; set; }
        [Required]
        public DateTime? Expirededate { get; set; }
        [Required]
        public float? Balance { get; set; }
        [Required]
        public int? Cardnumber { get; set; }
        [Required]
        public string Cardtype { get; set; }



    }
}
