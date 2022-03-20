using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using EventReservation.Core.Service;
using Microsoft.AspNetCore.Authorization;
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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;

        }

        [HttpGet]
        [Route("GetAllRoles")]
        [Authorize (Roles ="MainAdmin")]
        public IActionResult  GetAllRoles()//Done
        {
            var roles = _roleService.GetAllRoles();

            if (roles == null) return NoContent();
            return Ok(roles);

        }
        [HttpPost]
        [Route("AddRole")]
        public IActionResult AddRole([FromBody] Role role)//Done
        {
            var roles = _roleService.AddRole(role);

            if (roles ==false)
                return BadRequest("No");
            return Ok(roles);


        }
        [HttpDelete]
       [Route("DeleteRole/{id}")]
        [Authorize(Roles = "MainAdmin")]
        public IActionResult DeleteRole(int id)//Done
        {
            var roles = _roleService.DeleteRole(id);
            if (id == 0) return BadRequest();
            else if (roles == true) return Ok(roles);
            else
                return BadRequest("No");
                




        }
        [HttpGet]
        [Route("GetRoleById/{Id}")]//Done
        public IActionResult GetRoleById(int Id)
        {
            var roles = _roleService.GetRoleById(Id);
            if (Id == 0) return BadRequest();
            else if (roles !=null) return Ok(roles);
            else
                return NoContent();



        }



        [HttpPut]
        [Route("UpdateRole")]
        public IActionResult UpdateRole([FromBody] RoleToUpdateDTO Rolto)//Done
        {
            var roles = _roleService.UpdateRole(Rolto);
            if (roles == true) return Ok();
            else return NotFound();



        }





















    }
}
