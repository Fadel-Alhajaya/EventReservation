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
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;

        }

        [HttpGet]
        [Route("GetAllLogin")]
        public IActionResult GetAllLogin()//Done
        {
            var logins = _loginService.GetAllLogin();

            if (logins == null) return NoContent();
            return Ok(logins);

        }
        [HttpPost]
        [Route("AddLogin")]
        public IActionResult AddLogin([FromBody] Login login)//Done
        {
            var logins = _loginService.AddLogin(login);

            if (logins == false) return BadRequest();
            return Ok(logins);


        }

        [HttpDelete]
        [Route("DeleteLogin/{id}")]//Done
        public IActionResult DeleteLogin(int id)
        {
            var logins = _loginService.DeleteLogin(id);
            if (id == 0) return BadRequest();
            else if (logins == true) return Ok(logins);
            else
                return BadRequest("No");





        }
        [HttpGet]
        [Route("GetLoginById/{Id}")]
        public IActionResult GetLoginById(int Id)//Done
        {
            var logins = _loginService.GetLoginById(Id);
            if (Id == 0) return BadRequest();
            else if (logins != null) return Ok(logins);
            else
                return NoContent();



        }
        [HttpPut]
        [Route("UpdateLogin")]
        public IActionResult UpdateLogin([FromBody] LoginToUpdateDTO loginToUpdate)//Done
        {
            var logins = _loginService.UpdateLogin(loginToUpdate);
            if (logins == true) return Ok();
            else return NotFound();



        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Auth([FromBody] Login login)//Done
        {
            var token = _loginService.Auth(login);
            if (token == null) return Unauthorized();
            else
            {
                return Ok(token);
            }
        }







    }
}
