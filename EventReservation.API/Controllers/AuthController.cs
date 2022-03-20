using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EventReservation.Core.DTO;
using EventReservation.Core.Helpers;
using EventReservation.Core.Repository;
using EventReservation.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorePolicy")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary Cloudinary;
        public AuthController(IAuthService authService, IOptions<CloudinarySettings> CloudinaryConfig)
        {

            _authService = authService;


            _cloudinaryConfig = CloudinaryConfig;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
                );
            Cloudinary = new Cloudinary(acc);

        }
        [HttpPost]
        [Route("Register")]
       // [Authorize(Roles = "NormalUser")]

        public async Task<IActionResult> Register([FromForm] UserToRegisterDto userToRegisterDto)
        {
            userToRegisterDto.Username = userToRegisterDto.Username.ToLower();
            userToRegisterDto.Email = userToRegisterDto.Email.ToLower();

            if ( (await _authService.EmailExsists(userToRegisterDto.Email)) && (await _authService.UserNameExsists(userToRegisterDto.Username)))
                return BadRequest("Email or Username already exists ");

           // userToRegisterDto.PublicId = String.Empty;
            if (userToRegisterDto.ImgFile != null)
            {
                AddImage(userToRegisterDto.ImgFile, out string pubId, out string newPath);
                userToRegisterDto.Image = newPath;
                userToRegisterDto.PublicId = pubId;
            }
           var auth= _authService.Register(userToRegisterDto);
            return Ok(new
            {
                token = auth,});
        }

        
        private void AddImage(IFormFile img, out string pubId, out string newPath)
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
            newPath = uploadResult.Uri.ToString();
            pubId = uploadResult.PublicId;


        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login(UserToLoginDto userToLoginDto)
        {
            userToLoginDto.UserName = userToLoginDto.UserName.ToLower();
        
            var auth= _authService.Login(userToLoginDto);
            if (auth == "erorr")
                return Unauthorized("invalid Password or Username");
        

            return Ok(new
            {
                token = auth,
            });



        }


        [HttpPut]
        [Route("ChangePassword")]
        [Authorize("NormalUser,Admin")]
        public IActionResult ChangePassword(PasswordToDto passwordToDto)
        {
            var loginId = int.Parse(User.FindFirst(ClaimTypes.Sid).Value);
            passwordToDto.Logid = loginId;

            _authService.ChangePassword(passwordToDto);

            return Ok("passowrd has Changed");
        }
        //[HttpPost("SendMessage")]
        //public IActionResult SendMessage()
        //{
        //    string accountSid = Environment.GetEnvironmentVariable("twiliosid");
        //    string authToken = Environment.GetEnvironmentVariable("twilioauth");


        ////TwilioClient.Init(accountSid, authToken);

        ////    var messageOptions = new CreateMessageOptions(
        ////               new PhoneNumber("+962796698836"));
        ////    messageOptions.From = "+16812529148";
        ////    messageOptions.Body = "HI";

        ////    var message = MessageResource.Create(messageOptions);

        ////    Console.WriteLine(message.Sid);
        //   return Ok("message");

        //}




    }
}
