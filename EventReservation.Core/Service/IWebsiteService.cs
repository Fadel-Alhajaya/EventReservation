using EventReservation.Core.Data;
using EventReservation.Core.DTO.WebsiteDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Core.Service
{
   public interface IWebsiteService
    {
        Task<List<Website>> GetAllWebsite();

        Task<bool> AddWebsite(AddToWebsiteDto addToWebsiteDto);

        Task<bool> UpdateWebsite(UpdateToWebsiteDto updateToWebsiteDto);

        Task<bool> DeleteWebsite(int id);

        Task<Website> GetWebsiteById(int id);

        Task<List<Website>> SearchWebsiteByName(string name);


        Task<bool> EmailExsists(string email);





    }
}
