using EventReservation.Core.Data;
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
    public class LocationController : ControllerBase
    {

        private readonly ILoctationService _locationService;

        public LocationController(ILoctationService locationService)
        {
            _locationService = locationService;
        }


        [Route("AllLocation")]
        public IActionResult AllLocation()
        {
            var result = _locationService.GetAllLocation();
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);

        }
        [HttpPost]
        [Route("setLocation")]
        public IActionResult SetLocation(Location location)
        {
            var loc = _locationService.SetLocation(location); 
            if (loc == false)
                return BadRequest("Error On Setting Location");

            return Ok("The Location Added Successfully");

        }


        [HttpDelete]
        [Route("deleteLocation/{id}")]
        public IActionResult DeleteLocation(int id)
        {
            if (id == 0)
                return BadRequest();
            var flag = _locationService.DeleteLocation(id);
            if (flag == false)
                return BadRequest("Delete Process Fail");

            return Ok("Location Deleted Successfully");
        }

        [HttpGet]
        [Route("GetLocationById/{Id}")]//Done
        public IActionResult GetLocationById(int Id)
        {
            var location = _locationService.GetLocationById(Id);
            if (location == null)
                return BadRequest("No Location !!");

            return Ok(location);




        }
        [HttpPut]
        [Route("UpdateLocation/{Id}")]//Done
        public IActionResult GetLocationById(Location Address)
        {
            var Result = _locationService.UpdateLocation(Address);
            if (Result == false)
                return BadRequest("Can't Update Location !!");

            return Ok("Location Edit Successfully ");




        }

        [HttpGet]
        [Route("GetLocationCity/{city}")]
        public IActionResult GetLocationById( string city)
        {
            var location = _locationService.GetlocationIdByCity(city);
            if (location == null)
                return BadRequest("No Location !!");

            return Ok(location);

        }

        [HttpPost]
        [Route("GetLocationAddress")]
        public IActionResult GetLocationById(LocationToSearch locationToSearch)
        {
            var location = _locationService.GetlocationIdByAddress(locationToSearch.Longitude.ToString(), locationToSearch.Latitude.ToString());
            if (location == null)
                return BadRequest("No Location !!");

            return Ok(location);

        }
    }
}

