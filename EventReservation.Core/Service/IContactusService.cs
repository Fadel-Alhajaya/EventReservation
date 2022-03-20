using EventReservation.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.Service
{
   public interface IContactusService
    {
        List<Contactus>GetAllContact();
        bool CreateContact(Contactus contact);
        bool DeleteContact(int id);
        Contactus GetContactById(int id);
    }
}
