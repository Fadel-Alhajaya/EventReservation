using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using EventReservation.Core.Helpers;
using EventReservation.Core.Repository;
using EventReservation.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary Cloudinary;

        public UserController(IUserService userRepository, IOptions<CloudinarySettings> CloudinaryConfig)
        {

            _userService = userRepository;

            _cloudinaryConfig = CloudinaryConfig;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
                );
            Cloudinary = new Cloudinary(acc);

        } 

        [HttpGet]
        [Route("GetAllAdmin")]
        [Authorize(Roles ="MainAdmin")]
        public  IActionResult GetAllAdmin()
        {
            var allUser =  _userService.GetAllUsers().Result;
            var admin =  from data in allUser
                         where data.Position == "Admin"
                         select new UsertoResultDto
                        {
                            Firstname = data.Firstname,
                            Lastname = data.Lastname,
                            Image = data.Image,
                            Email=data.Email,
                            Birthdate=data.Birthdate,
                            Position=data.Position,
                            Username=data.Username
                            
                        };
         
            if (admin.Count() == 0)
                return NoContent();


            return Ok(admin.ToList());
          


        }

        [HttpGet]
        [Route("GetAllUser")]
        public IActionResult GetAllUser()
        {
            var allUser = _userService.GetAllUsers().Result;
            var user = from data in allUser
                        where data.Position == "NormalUser"
                        select new UsertoResultDto
                        {

                            Firstname = data.Firstname,
                            Lastname = data.Lastname,
                            Image = data.Image,
                            Email = data.Email,
                            Birthdate = data.Birthdate,
                            Position = data.Position,
                            Username = data.Username

                        };





            if (user.Count()==0)
                return NoContent();


            return Ok(user.ToList());



        }
        [HttpGet]
        [Route("Profile")]
        [Authorize(Roles ="Admin,NormalUser")]
        public IActionResult ViewProfile()
        {
            var userId = int.Parse(User.FindFirst((ClaimTypes.NameIdentifier)).Value);
            var user = _userService.GetUserById(userId).Result;
             
            if (user != null)
                return Ok(user);

            return Unauthorized();
            


        }

        [HttpPut]
        [Route("EditProfile")]
        [Authorize(Roles =("Admin,NormalUser"))]
        public async Task<IActionResult> EditProfile([FromForm]UserToUpdateDto userToUpdateDto)
        {
            var userId = int.Parse(User.FindFirst((ClaimTypes.NameIdentifier)).Value);
            userToUpdateDto.Userid=userId;
            var oldUserData = _userService.GetUserById(userId).Result;
            if(userToUpdateDto.Email !=oldUserData.Email)
            if (await _userService.EmailExsists(userToUpdateDto.Email))
                return BadRequest("Email is already exists");
            if (userToUpdateDto.ImageFile != null) { 
            DeleteImage(oldUserData.PublicId);
            AddImage(userToUpdateDto.ImageFile, out string pubid, out string newpath);
            userToUpdateDto.Image = newpath;
            userToUpdateDto.PublicId = pubid;
            }
            await _userService.EditProfile(userToUpdateDto);

            return Ok(userToUpdateDto);



        }

       
        private void DeleteImage(string publicId)
        {
            
            if (publicId != null)
            {
                var deleteParams = new DeletionParams(publicId);
                var result = Cloudinary.Destroy(deleteParams);
               

            }
            

        }

        [HttpPost]
        [Route("AddAddmin")]
        [Authorize("MainAdmin")]
        public async Task<IActionResult> AddAdmin([FromForm] AddToAdminDto addToAdminDto)
        {
            addToAdminDto.Username = addToAdminDto.Username.ToLower();
            addToAdminDto.Email = addToAdminDto.Email.ToLower();
            if ((await _userService.EmailExsists(addToAdminDto.Email)) && (await _userService.UserNameExsists(addToAdminDto.Username))) 
                return BadRequest("Email or Username  already exists ");
           
            AddImage(addToAdminDto.ImageFile,out string pubid ,out string newpath);
            addToAdminDto.Image = newpath;
            addToAdminDto.PublicId= pubid;
           await _userService.AddAdmin(addToAdminDto);

           return Ok("Admin Added Successfully ");
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
                        Folder = "User",
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face"),
                        
                        
                    };
                  
                    uploadResult = Cloudinary.Upload(uploadParams);

                };
            }
             newpath = uploadResult.Uri.ToString();
             pubid = uploadResult.PublicId;
          

        }





        [HttpGet]
        [Route("SearchUser")]
        [Authorize(Roles = ("Admin,MainAdmin"))]
        public IActionResult SearchUser(UserToSearchDto userToSearchDto)
        {
            var result = _userService.SearchUser(userToSearchDto).Result;
            if (result == null)
                return NoContent();

            return Ok(result);



        }


        [HttpDelete]
        [Route("DeleteUser/{id}")]
        [Authorize(Roles =("Admin,MainAdmin"))]
        public IActionResult DeleteUser(int id)
        {
            var delete = _userService.GetUserById(id).Result;
            if (delete ==null)
                return BadRequest("the user is not found");

            DeleteImage(delete.PublicId);
            _userService.DeleteUser(id);
            return Ok("the user is deleted");
        }



    }
}
