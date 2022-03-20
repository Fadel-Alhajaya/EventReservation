using EventReservation.Core.Data;
using EventReservation.Core.Repository;
using EventReservation.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Infra.Service
{
    public class ContactusService : IContactusService
    {
        private readonly IContactusRepository _contactusRepository;
        public ContactusService(IContactusRepository contactusRepository)
        {
            _contactusRepository = contactusRepository;
        }
        public bool CreateContact(Contactus contact)
        {
            return _contactusRepository.CreateContact(contact);
        }

        public bool DeleteContact(int id)
        {
            return _contactusRepository.DeleteContact(id);
        }

        public List<Contactus> GetAllContact()
        {
            return _contactusRepository.GetAllContact();
        }

        public Contactus GetContactById(int id)
        {
            return _contactusRepository.GetContactById(id);
        }
    }
}
