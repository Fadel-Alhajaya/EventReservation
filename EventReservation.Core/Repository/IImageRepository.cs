using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.Repository
{
    public interface IImageRepository
    {
        List<Image> GetAllImage();
        bool AddImage(ImageToAddDto imageToAddDto);
        bool UpdateImage(ImageToUpdateDto imageToUpdateDto);
        bool DeleteImage(int ImageId);
        Image GetImageById(int ImageId);
        Image GetImageByUrl(string  Url);
        List<Image> GetImageByHall(int  Hallid);
    }
}
