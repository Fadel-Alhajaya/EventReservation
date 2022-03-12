using EventReservation.Core.DTO;
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
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

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
            
               bool flag= _eventService.AddNewEvent(eventToAddDto);
                if(flag!=true)
                    return BadRequest("Error in Reservation Process ");

                return Ok("Waiting For Accept Your Event");

        
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
            bool flag = _eventService.AcceptEvent(Id);
            if (flag == false)
                return BadRequest("Not Accepted");

            return Ok("Event Accepted Successfully");


        }
    }
}
