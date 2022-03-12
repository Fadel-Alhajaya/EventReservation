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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;

        }
        [HttpPost]
        [Route("AddReview")]
        public IActionResult AddReview([FromBody]Review review)
        {
            var revs = _reviewService.AddReview(review);
            if (revs == false) return NoContent();
            return Ok(revs);

          
        }
        [HttpGet]
        [Route("GetAllReviews")]
        public  IActionResult GetAllReviews()
        {
            var revs = _reviewService.GetAllReviews();
            if (revs == null) return NoContent();

            return Ok(revs);
        }
        //[HttpGet]
        //[Route("GetAvgReviews")]
        //public List<AvgRateDTO> GetAvgRate()
        //{

        //    return _reviewService.GetAvgRate();
        //}




    }
}
