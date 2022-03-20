using EventReservation.Core.Data;
using EventReservation.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactusController : ControllerBase
    {
        private readonly IContactusService _contactusService;
        public ContactusController(IContactusService contactusService)
        {
            _contactusService = contactusService;
        }

        [HttpGet]
        [Route("GetAllContacts")]
        public IActionResult GetAllContactus()
        {
            var contacts = _contactusService.GetAllContact();

            if (contacts == null) return NoContent();
            return Ok(contacts);
        }

        [HttpPost]
        [Route("AddContact")]
        public IActionResult AddContact([FromBody] Contactus contact)//Done
        {
            var contacts = _contactusService.CreateContact(contact);

            if (contacts == false) return BadRequest();
            return Ok(contacts);

        }

        [HttpDelete]
        [Route("DeleteContact/{id}")]
        public IActionResult DeleteContact(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var contact = _contactusService.GetContactById(id);
            if (contact == null)
            {
                return BadRequest("Contact Does Not Exist");
            }
            var flag = _contactusService.DeleteContact(id);
            if (flag == false)
                return BadRequest("Delete Process Fail");

            return Ok("Contact Deleted Successfully");
        }

        [HttpGet]
        [Route("GetContactById/{Id}")]//Done
        public IActionResult GetEventById(int Id)
        {
            var result = _contactusService.GetContactById(Id);
            if (result == null)
                return BadRequest("No Contact !!");

            return Ok(result);


        }
    }
}
