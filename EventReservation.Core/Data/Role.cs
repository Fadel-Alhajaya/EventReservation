using System;
using System.Collections.Generic;

namespace EventReservation.Core.Data
{
    public  class Role
    {
       

        public int id { get; set; }
        public string Position { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
