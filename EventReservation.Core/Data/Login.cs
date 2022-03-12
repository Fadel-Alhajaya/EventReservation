using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventReservation.Core.Data
{
    public  class Login
    {
        [Key]
        public int id { get; set; }
        public string Username { get; set; }
        public byte[] Passwordhash { get; set; }
        public byte[] Passwordsalt { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
