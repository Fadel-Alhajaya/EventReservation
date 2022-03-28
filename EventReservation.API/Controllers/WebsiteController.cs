using EventReservation.Core.DTO.WebsiteDto;
using EventReservation.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebsiteController : ControllerBase
    {
        private readonly IWebsiteService _websiteService;
        private readonly IWebHostEnvironment _hostEnviroment;
        public WebsiteController(IWebsiteService websiteService, IWebHostEnvironment webHost)
        {
            _hostEnviroment = webHost;
            _websiteService = websiteService;
        }


        [HttpGet]
        [Route("GetAllWebsite")]
        //[Authorize(Roles =("Admin,MainAdmin"))]
        public IActionResult GetAllWebsite()
        {
            var result = _websiteService.GetAllWebsite().Result;


            var admin = from data in result 
                        select new 
                        {
                            Websiteid = data.Websiteid,
                            Websitename = data.Websitename,
                            Wallet = data.Wallet,
                            Telephone = data.Telephone,
                            Email = data.Email,
                            Backgroundimg = data.Backgroundimg,
                            Address = data.Address,
                            Username = data.Logopath

                        };
            if (result == null)
                return NoContent();

            return Ok(result);

        }

        [HttpPost]
        [Route("AddWebsite")]
        [Authorize(Roles = ("Admin,MainAdmin"))]
        public async Task<IActionResult> AddWebsite([FromForm] AddToWebsiteDto addWebsiteDto)
        {


            addWebsiteDto.Email = addWebsiteDto.Email.ToLower();
            var adminid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            addWebsiteDto.Adminid = adminid;
            addWebsiteDto.Backgroundimg = UploadImage("Backgroundimg");
            addWebsiteDto.Logopath = UploadImage("Logopath");

            if (await _websiteService.EmailExsists(addWebsiteDto.Email))
                return BadRequest("The email is already exsist");

            await _websiteService.AddWebsite(addWebsiteDto);

            return Ok("Website Created");

        }


        [HttpPut]
        [Route("UpdateWebsite")]
        [Authorize(Roles = ("Admin,MainAdmin"))]
        public async Task<IActionResult> UpdateWebsite([FromForm] UpdateToWebsiteDto updateToWebsiteDto)
        {

            updateToWebsiteDto.Email = updateToWebsiteDto.Email.ToLower();
            var adminid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var webOldResult = _websiteService.GetWebsiteById(updateToWebsiteDto.Websiteid).Result;

            updateToWebsiteDto.Adminid = adminid;

            var projectPath = _hostEnviroment.ContentRootPath;
            if (webOldResult.Backgroundimg != null)
            {
                string nameBackGround = Path.GetFileName(webOldResult.Backgroundimg);

                var oldPathBackGround = Path.Combine(projectPath, "Images/Website/", nameBackGround);

                System.IO.File.Delete(oldPathBackGround);

            }

            if (webOldResult.Logopath != null)
            {

                string nameLogo = Path.GetFileName(webOldResult.Logopath);

                var oldPathLogo = Path.Combine(projectPath, "Images/Website/", nameLogo);

                System.IO.File.Delete(oldPathLogo);

            }

            updateToWebsiteDto.Backgroundimg = UploadImage("Backgroundimg");


            updateToWebsiteDto.Logopath = UploadImage("Logopath");



            if (webOldResult.Email != updateToWebsiteDto.Email)
                if (await _websiteService.EmailExsists(updateToWebsiteDto.Email))
                    return BadRequest("The email is already exsist");

            await _websiteService.UpdateWebsite(updateToWebsiteDto);

            return Ok("Website Updated");

        }


        private string UploadImage(string name)
        {
            try
            {
                var file = Request.Form.Files[name];
                if (file != null)
                {

                    var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var fullPath = Path.Combine("Images/Website", fileName);
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


        [HttpDelete]
        [Route("DeleteWebsite/{id}")]
        [Authorize("Admin")]
        public IActionResult DeleteWebsite(int id)
        {
            var delete = _websiteService.GetWebsiteById(id).Result;
            if (delete == null)
                return BadRequest("the website is not found");
            _websiteService.DeleteWebsite(id);
            return Ok("the user is deleted");
        }




    }
}