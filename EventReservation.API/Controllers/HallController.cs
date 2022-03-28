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
    public class HallController : ControllerBase
    {
        private readonly IHallService _hallService;


        public HallController(IHallService hallService)
        {
            _hallService = hallService;
        }

        [HttpGet]
        [Route("GetAllHalls")]
        public IActionResult GetAllHalls()
        {
            var result = _hallService.GetAllHall();

            if (result == null) return NoContent();
            return Ok(result);
        }


        [HttpPost]
        [Route("AddHall")]
        public IActionResult AddHall([FromBody] Hall hall)
        {
            var result = _hallService.CreateHall(hall);

            if (result == null)
                return BadRequest();
            return Ok(result);

        }

        [HttpDelete]
        [Route("DeleteHall/{id}")]
        public IActionResult DeleteHall(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var result = _hallService.GetHallById(id);
            if (result == null)
            {
                return BadRequest("Hall Does Not Exist");
            }
            var flag = _hallService.DeleteHall(id);
            if (flag == false)
                return BadRequest("Delete Process Fail");

            return Ok("Hall Deleted Successfully");
        }

        [HttpGet]
        [Route("GetHallById/{Id}")]
        public IActionResult GetHallById(int Id)
        {
            var result = _hallService.GetHallById(Id);
            if (result == null)
                return BadRequest("No Hall !!");

            return Ok(result);


        }

        [HttpGet]
        [Route("GetHallByName/{name}")]//Done
        public IActionResult GetHallByName(string name)
        {
            var result = _hallService.GetHallByName(name);
            if (result == null)
                return BadRequest("No Hall !!");

            return Ok(result);


        }


        [HttpPut]
        [Route("UpdateHall")]
        public IActionResult UpdateHall([FromBody] Hall hall)//Done
        {
            var result = _hallService.UpdateHall(hall);

            if (result == false) return BadRequest();
            return Ok(result);

        }


        [HttpGet]
        [Route("GetCheapestHall")]
        public IActionResult GetCheapestHall()
        {
            var result = _hallService.GetCheapestHall();
            if (result == null)
                return BadRequest("No Hall !!");

            return Ok(result);


        }


        [HttpGet]
        [Route("GetHallByCapacity/{CAP}")]//Done
        public IActionResult GetHallByCapacity(int CAP)
        {
            var result = _hallService.GetHallByCapacity(CAP);
            if (result == null)
                return BadRequest("No Hall !!");

            return Ok(result);


        }


        [HttpGet]
        [Route("GetHallByPrice/{price}")]//Done
        public IActionResult GetHallByPrice(int price)
        {
            var result = _hallService.GetHallByPrice(price);
            if (result == null)
                return BadRequest("No Hall !!");

            return Ok(result);

        }


        [HttpGet]
        [Route("GetHallByLocationId/{Id}")]//Done
        public IActionResult GetHallByLocationId(int Id)
        {
            var result = _hallService.GetHallByLocationId(Id);
            if (result == null)
                return BadRequest("No Hall !!");

            return Ok(result);


        }


        [HttpGet]
        [Route("GetHallByUsage/{usage}")]//Done
        public IActionResult GetHallByUsage(string usage)
        {
            var result = _hallService.GetHallByUsage(usage);
            if (result == null)
                return BadRequest("No Hall !!");

            return Ok(result);


        }
    }
}
