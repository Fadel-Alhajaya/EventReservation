using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Core.Repository
{
    public interface IAboutusRepository
    {

         Task<List<AboutusToDto>> GetAllAboutus();

         Task<Aboutus> AddAboutus(Aboutus aboutus);

         Task<bool> UpdateAboutus(UpdateAboutusToDto aboutus);

        Task<bool> DeleteAboutus(int id);


        Task<Aboutus> GetAboutusById(int id);



    }
}
