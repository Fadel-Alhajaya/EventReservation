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
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary Cloudinary;
        public ImageController(IImageService imageService,IOptions<CloudinarySettings>CloudinaryConfig)
        {
            _imageService = imageService;
            _cloudinaryConfig = CloudinaryConfig;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
                );
            Cloudinary = new Cloudinary(acc);
                
        }
        [HttpPost]
        [Route("AddImage")]
        public IActionResult AddImage([FromForm]ImageToAddDto imageToAddDto)
        {

            foreach (var item in imageToAddDto.ImageFile)
            {
                var file = item;
                var uploadResult = new ImageUploadResult();
                if (file.Length > 0)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {

                            File = new FileDescription(file.Name, stream),
                            Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                        };
                        uploadResult = Cloudinary.Upload(uploadParams);

                    }
                }
                imageToAddDto.ImageUrl = uploadResult.Uri.ToString();
                imageToAddDto.Publicid = uploadResult.PublicId;
                _imageService.AddImage(imageToAddDto);
            }
            return Ok("Image Successfully Created");

        }
       
        
        [Route("AllImages")]
        public IActionResult AllImage()
        {
            var result = _imageService.GetAllImage();
            if(result==null)
            {
                return NoContent();
            }
            return Ok(result);

        }

        [HttpDelete]
        [Route("deleteImage/{id}")]
        public IActionResult DeleteImage(int id)
        {
            if (id == 0)
                return BadRequest();
            var image = _imageService.GetImageById(id);
            if(image==null)
            {
                return BadRequest("Image You Try to Delete NotFound");
            }
            if (image.PublicId != null)
            {
                var deleteParams = new DeletionParams(image.PublicId);
                var result = Cloudinary.Destroy(deleteParams);
                if (result.Result == "ok")
                {
                    bool flag = _imageService.DeleteImage(id);
                    if (flag == false)
                        return BadRequest("Error in the Delete proccess");
                   
                }

                
            }
            else
            {
                bool flag = _imageService.DeleteImage(id);
                if (flag == false)
                    return BadRequest("Error in the Delete proccess");
               
            }
            return Ok("The Image successfully Deleted");







        }

        [HttpGet]
        [Route("GetImageById/{Id}")]//Done
        public IActionResult GetImageById(int Id)
        {
            var image = _imageService.GetImageById(Id);
            if (Id == 0)
                return BadRequest();
            else if (image != null)
                return Ok(image);
            else
                return NoContent();



        }
        [HttpGet]
        [Route("GetImageByHall/{Id}")]//Done
        public IActionResult GetImageByHall(int Id)
        {
            if (Id == 0)
                return BadRequest();

            var image = _imageService.GetImageByHall(Id);
            
           if (image == null)
                return NoContent();

            return Ok(image);

        }

    }
}
