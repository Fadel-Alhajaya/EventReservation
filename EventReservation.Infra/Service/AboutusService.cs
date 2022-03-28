using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using EventReservation.Core.Repository;
using EventReservation.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Infra.Service
{
    public class AboutusService : IAboutusService
    {
        private readonly IAboutusRepository _aboutusRepository;

        public AboutusService(IAboutusRepository aboutusRepository)
        {

            _aboutusRepository = aboutusRepository;
        }
        public Task<Aboutus> AddAboutus(Aboutus aboutus)
        {
            return _aboutusRepository.AddAboutus(aboutus);
        }

        public Task<bool> DeleteAboutus(int id)
        {
            return _aboutusRepository.DeleteAboutus(id);
        }

        public Task<Aboutus> GetAboutusById(int id)
        {
            return _aboutusRepository.GetAboutusById(id);
        }

        public Task<List<AboutusToDto>> GetAllAboutus()
        {
            return _aboutusRepository.GetAllAboutus();
        }

        public Task<bool> UpdateAboutus(UpdateAboutusToDto aboutus)
        {
            return _aboutusRepository.UpdateAboutus(aboutus);
        }
    }
}
