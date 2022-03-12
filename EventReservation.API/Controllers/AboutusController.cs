using EventReservation.Core.Data;
using EventReservation.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EventReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutusController : ControllerBase
    {

        private readonly IAboutusService _aboutusService;
        private readonly IWebHostEnvironment _hostEnviroment;


        public AboutusController(IAboutusService aboutusService, IWebHostEnvironment webHostEnvironment)
        {

            _aboutusService = aboutusService;
            _hostEnviroment = webHostEnvironment;
        }

        [HttpGet]
        [Route("GetAboutUS")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAboutUs()
        {
            var result = _aboutusService.GetAllAboutus().Result;

            if (result == null)
                return NoContent();

            return Ok(result);

        }


        [HttpPost]
        [Route("CreateAboutUS")]
        [Authorize(Roles ="Admin")]
        public IActionResult AddAboutus([FromForm] Aboutus aboutus)
        {
            var result = _aboutusService.AddAboutus(aboutus);
            aboutus.Imagepath = UploadImage();
            if (result == null)
                return BadRequest();

            return Ok("Done");

        }

        private string UploadImage()
        {
            try
            {

                var file = Request.Form.Files["Imagepath"];
                if (file != null)
                {

                    var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var fullPath = Path.Combine("Images/Aboutus", fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);

                    }
                    return fileName;
                }
                return null;
            }
            catch (Exception e)
            {
                return e.Message;

            }
        }


        [HttpPut]
        [Route("UpdateAboutUS")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateAboutus([FromForm]Aboutus aboutus)
        {
            var adoutOldResult = _aboutusService.GetAboutusById(aboutus.Aboutusid).Result;
            var projectPath = _hostEnviroment.ContentRootPath;
            if (adoutOldResult.Imagepath != null)
            {
                string nameImg = Path.GetFileName(adoutOldResult.Imagepath);

                var oldPathImg = Path.Combine(projectPath, "Images/Aboutus/", nameImg);

                System.IO.File.Delete(oldPathImg);
            }
            aboutus.Imagepath = UploadImage();
            var result = _aboutusService.UpdateAboutus(aboutus);
            if (result == null)
                return BadRequest();
       
            return Ok("Done");
            
        }

        [HttpGet]
        [Route("GetAboutById/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAboutusById(int id)
        {
            var result = _aboutusService.GetAboutusById(id);

           
            if (result == null)
                return BadRequest();

            return Ok(result);

        }

        [HttpDelete]
        [Route("DeleteAboutus/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteAboutus(int id)
        {
            var delete = _aboutusService.GetAboutusById(id).Result;
            if (delete == null)
                return BadRequest("The aboutus is not found");
            _aboutusService.DeleteAboutus(id);
            return Ok("the user is deleted");
           

        }

       




    }
}
