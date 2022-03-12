using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using EventReservation.Core.Repository;
using EventReservation.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Infra.Service
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        public bool AddImage(ImageToAddDto imageToAddDto)
        {
            return _imageRepository.AddImage(imageToAddDto);
        }

        public bool DeleteImage(int ImageId)
        {
            return _imageRepository.DeleteImage(ImageId);
        }

        public List<Image> GetAllImage()
        {
            return _imageRepository.GetAllImage();
        }

        public List<Image> GetImageByHall(int Hallid)
        {
            return _imageRepository.GetImageByHall(Hallid);
        }

        public Image GetImageById(int ImageId)
        {
            return _imageRepository.GetImageById(ImageId);
         }

        public Image GetImageByUrl(string Url)
        {
            return _imageRepository.GetImageByUrl(Url);
        }

        public bool UpdateImage(ImageToUpdateDto imageToUpdateDto)
        {
            return _imageRepository.UpdateImage(imageToUpdateDto);
        }
    }
}
