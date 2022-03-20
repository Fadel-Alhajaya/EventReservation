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
    public class CardController : ControllerBase
    {

        private readonly ICardService _cardService;
        public CardController(ICardService cardService)
        {
            _cardService = cardService;

        }

        [HttpGet]
        [Route("GetAllCard")]
        public IActionResult GetAllRoles()//Done
        {
            var cards = _cardService.GetAllCards();

            if (cards == null) return NoContent();
            return Ok(cards);

        }
        [HttpPost]
        [Route("AddCard")]
        public IActionResult AddRole([FromBody] Card card)//Done
        {
            var cards = _cardService.AddCard(card);

            if (cards == false) return BadRequest();
            return Ok(cards);


        }
        [HttpDelete]
        [Route("DeleteCard/{id}")]
        [Authorize (Roles="NormalUser")]
        public IActionResult DeleteCard(int id)//Done
        {
            var cards = _cardService.DeleteCard(id);
            if (id == 0) return BadRequest();
            else if (cards == true) return Ok(cards);
            else
                return BadRequest("No");





        }
        [HttpGet]
        [Route("GetCardById/{Id}")]//Done
        [Authorize(Roles = "NormalUser")]
        public IActionResult GetRoleById(int Id)
        {
            var cards = _cardService.GetCardById(Id);
            if (Id == 0) return BadRequest();
            else if (cards != null) return Ok(cards);
            else
                return NoContent();



        }



        [HttpPut]
        [Route("UpdateCard")]
        public IActionResult UpdateRole([FromBody] CardToUpdateDTO cardToUpdateDTO)//Done
        {
            var roles = _cardService.UpdateCard(cardToUpdateDTO);
            if (roles == true) return Ok();
            else return NotFound();



        }




    }
}
