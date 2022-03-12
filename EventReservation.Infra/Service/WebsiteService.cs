using EventReservation.Core.Data;
using EventReservation.Core.DTO.WebsiteDto;
using EventReservation.Core.Repository;
using EventReservation.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Infra.Service
{
    public class WebsiteService : IWebsiteService
    {
        private readonly IWebsiteRepository _websiteRepositry ;

        public WebsiteService(IWebsiteRepository websiteRepository)
        {
            _websiteRepositry = websiteRepository;

        }

        public Task<bool> AddWebsite(AddToWebsiteDto addToWebsiteDto)
        {
            return _websiteRepositry.AddWebsite(addToWebsiteDto);
        }


        public Task<bool> UpdateWebsite(UpdateToWebsiteDto updateToWebsiteDto)
        {
            return _websiteRepositry.UpdateWebsite(updateToWebsiteDto);
        }

        public Task<bool> DeleteWebsite(int id)
        {
            return _websiteRepositry.DeleteWebsite(id);
        }

        public Task<bool> EmailExsists(string email)
        {
            return _websiteRepositry.EmailExsists(email);
        }

        public Task<List<Website>> GetAllWebsite()
        {
            return _websiteRepositry.GetAllWebsite();
        }

        public Task<Website> GetWebsiteById(int id)
        {
            return _websiteRepositry.GetWebsiteById(id);
        }

        public Task<List<Website>> SearchWebsiteByName(string name)
        {
            return _websiteRepositry.SearchWebsiteByName(name);
        }

    }
}
