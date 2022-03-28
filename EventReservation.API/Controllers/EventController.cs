using EventReservation.Core.DTO;
using EventReservation.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IUserService _userService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [Route("AllEvent")]
        public IActionResult AllEvent()
        {
            var result =_eventService.GetAllEvent();
            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);

        }
        [HttpPost]
        [Route("NewEvent")]
        public IActionResult AddNewEvent(EventToAddDto eventToAddDto)
        {
            //chick if user found //and check for no people
            bool result = _eventService.GetStatusOfHall(eventToAddDto.HallId,eventToAddDto.Startdate, eventToAddDto.Enddate);
            if (result == false)
            {
                return BadRequest("Can't Resarve The Hall At This Time");
            }
            var resultUser = _userService.GetUserById(eventToAddDto.UserId).Result;
            CardToDto cardToDto = new CardToDto();
            cardToDto.Cardname = eventToAddDto.Cardname;
            cardToDto.Cardnumber = eventToAddDto.Cardnumber;
            cardToDto.Ccv = eventToAddDto.Ccv;
            cardToDto.city = eventToAddDto.city;
            cardToDto.country = eventToAddDto.country;
            cardToDto.Expirededate = eventToAddDto.Expirededate;
            cardToDto.fullname = eventToAddDto.fullname;
            cardToDto.email = resultUser.Email;
            eventToAddDto.cardtokenid = cardTokenId(cardToDto);
            

               bool flag= _eventService.AddNewEvent(eventToAddDto);
                if(flag!=true)
                    return BadRequest("Error in Reservation Process ");

                return Ok("Waiting For Accept Your Event");

        
        }

        private string cardTokenId(CardToDto cardToDto)
        {

         
            var optiostoken = new TokenCreateOptions
            {

                Card = new TokenCardOptions
                {
                    Name = cardToDto.Cardname,
                    AddressCountry = cardToDto.country,
                    AddressCity = cardToDto.city,
                    Number = cardToDto.Cardnumber,
                    ExpMonth = cardToDto.Expirededate.Value.Month,
                    ExpYear = cardToDto.Expirededate.Value.Year,
                    Cvc = cardToDto.Ccv,

                },

            };

            //for create unique token 
            var servicetoken = new TokenService();
            Token token =  servicetoken.Create(optiostoken);

            var options = new CustomerCreateOptions
            {
                Description = "Customer (Eventreservation)",
                Email = cardToDto.email,
                Name = cardToDto.fullname,
                Source = token.Id,

            };
            var service = new CustomerService();
            var cus = service.Create(options);



            return (cus.Id);
        }

        [HttpDelete]
        [Route("deleteDeleteEvent/{id}")]
        public IActionResult DeleteEvent(int id)
        {
            if (id == 0)
                return BadRequest();
            var flag = _eventService.DeleteEvent(id);
            if (flag == false)
                return BadRequest("Delete Process Fail");

            return Ok("Event Deleted Successfully");
        }
        [HttpGet]
        [Route("GetEventById/{Id}")]//Done
        public IActionResult GetEventById(int Id)
        {
            var result = _eventService.GetEventById(Id);

            var serviceCustomer = new CustomerService();
            var resultCustomer = serviceCustomer.Get(result.cardtokenid);
           
            var servicecard = new CardService();
            var resultCard = servicecard.Get(result.cardtokenid, resultCustomer.DefaultSourceId);
            result.Cardname = resultCard.Name;
            result.ExpMonth = resultCard.ExpMonth;
            result.ExpYear = resultCard.ExpYear;
            result.cardtype = resultCard.Brand;
            result.Ccv = resultCard.CvcCheck;
            result.Cardnumber = resultCard.Last4;
            result.city = resultCard.AddressCity;
            result.country = resultCard.AddressCountry;


            if (result == null)
                return BadRequest("No Event !!");

            return Ok(result);


        }
        [HttpGet]
        [Route("AcceptEvent/{Id}")]//Done
        public IActionResult AcceptEvent(int Id)
        {
            var _event = _eventService.GetEventById(Id);
            if (_event.Status == "Accepted")
                return Ok("Event already Accepted");
            var result = _eventService.AcceptEvent(Id);
            if (result == null)
                return BadRequest("Not Accepted");

            return Ok(result);


        }


        [HttpGet]
        [Route("RejectEvent/{Id}")]//Done
        public IActionResult RejectEvent(int Id)
        {
            var _event = _eventService.GetEventById(Id);
            if (_event.Status == "Rejected")
                return Ok("Event already Rejected");
            var result = _eventService.RejectEvent(Id);
            if (result == null)
                return BadRequest("Not Rejected");

            return Ok(result);


        }
    }
}
