using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EventReservation.Core.DTO;
using EventReservation.Core.Helpers;
using EventReservation.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {
        private readonly ITestimonialService _testimonialService;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary Cloudinary;

        public TestimonialController(ITestimonialService testimonialService, IOptions<CloudinarySettings> CloudinaryConfig)
        {
            _testimonialService = testimonialService;
            _cloudinaryConfig = CloudinaryConfig;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
                );
            Cloudinary = new Cloudinary(acc);
        }

        [HttpGet]
        [Route("GetAllTestimonials")]
        public IActionResult GetAllTestimonials()
        {
            var result = _testimonialService.GetAllTestimonial();

            if (result == null) return NoContent();
            return Ok(result);
        }
        [HttpGet]
        [Route("GetAllTestimonialApproved")]
        public IActionResult GetAllTestimonialApproved()
        {
            var result = _testimonialService.GetAllTestimonialApproved();

            if (result == null) return NoContent();
            return Ok(result);
        }

        private void AddImage(IFormFile img, out string pubid, out string newpath)
        {
            var file = img;
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        Folder = "Testimonials",
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face"),


                    };

                    uploadResult = Cloudinary.Upload(uploadParams);

                }
            }
            newpath = uploadResult.Uri.ToString();
            pubid = uploadResult.PublicId;


        }

        [HttpPost]
        [Route("AddTestimonial")]
        public IActionResult AddTestimonial([FromForm] ToAddTestimonial toAddTestimonial)
        {
            toAddTestimonial.Publicid = String.Empty;
            toAddTestimonial.Personalname = toAddTestimonial.Personalname.ToLower();
            AddImage(toAddTestimonial.ImageFile, out string pubid, out string newpath);
            toAddTestimonial.ImageUrl = newpath;
            toAddTestimonial.Publicid = pubid;
            _testimonialService.CreateTestimonial(toAddTestimonial);

            return Ok("Testimonial has been added successfully");
        }



        [HttpDelete]
        [Route("DeleteTestimonial/{id}")]
        public IActionResult DeleteTestimomial(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var test = _testimonialService.GetTestimonialById(id);
            if (test == null)
            {
                return BadRequest("Testimonial Does Not Exist");
            }
            if (test.PublicId != null)
            {
                var deleteParams = new DeletionParams(test.PublicId);
                var result = Cloudinary.Destroy(deleteParams);
                if (result.Result == "ok")
                {
                    bool flag = _testimonialService.DeleteTestimonial(id);
                    if (flag == false)
                        return BadRequest("Error in the Delete proccess");

                }


            }
            else
            {
                bool flag = _testimonialService.DeleteTestimonial(id);
                if (flag == false)
                    return BadRequest("Error in the Delete proccess");

            }

            return Ok("Testimonial Deleted Successfully");
        }



        [HttpGet]
        [Route("GetTestimonialById/{Id}")]
        public IActionResult GetTestimonialById(int Id)
        {
            var result = _testimonialService.GetTestimonialById(Id);
            if (result == null)
                return BadRequest("No Testimonial !!");

            return Ok(result);


        }

        [HttpGet]
        [Route("ApproveTestimonial/{Id}")]
        public IActionResult ApproveTestimonial(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var result = _testimonialService.GetTestimonialById(id);
            if (result == null)
            {
                return BadRequest("Testimonial Does Not Exist");
            }
            var flag = _testimonialService.ApproveTestimonial(id);
            if (flag == false)
                return BadRequest("Approve Process Fail");

            return Ok("Testimonial Approved Successfully");
        }


        [HttpGet]
        [Route("UnapproveTestimonial/{Id}")]
        public IActionResult UnapproveTestimonial(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var result = _testimonialService.GetTestimonialById(id);
            if (result == null)
            {
                return BadRequest("Testimonial Does Not Exist");
            }
            var flag = _testimonialService.UnapproveTestimonial(id);
            if (flag == false)
                return BadRequest("Unapprove Process Fail");

            return Ok("Testimonial Unapproved Successfully");
        }


    }
}
