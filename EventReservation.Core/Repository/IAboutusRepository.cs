using EventReservation.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Core.Repository
{
    public interface IAboutusRepository
    {

         Task<Aboutus> GetAllAboutus();

         Task<Aboutus> AddAboutus(Aboutus aboutus);

         Task<bool> UpdateAboutus(Aboutus aboutus);

        Task<bool> DeleteAboutus(int id);


        Task<Aboutus> GetAboutusById(int id);



    }
}
