using Dapper;
using EventReservation.Core.Common;
using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EventReservation.Core.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly IDbContext _dbContext;
        public ImageRepository (IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool AddImage(ImageToAddDto imageToAddDto)
        {
            var p = new DynamicParameters();
            p.Add("url", imageToAddDto.ImageUrl, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Descp", imageToAddDto.Description, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Hall", imageToAddDto.Hallid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("pid", imageToAddDto.Publicid, dbType: DbType.String, direction: ParameterDirection.Input);

            var result = _dbContext.Connection.ExecuteAsync("IMAGE_PACKAGE.CreateImage", p, commandType: CommandType.StoredProcedure);
            if (result == null) 
                return false;
            return true;
        }

        public bool DeleteImage(int ImageId)
        {
            var p = new DynamicParameters();
            p.Add("id", ImageId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = _dbContext.Connection.ExecuteAsync("IMAGE_PACKAGE.DeleteImage", p, commandType: CommandType.StoredProcedure);
            if (result == null) return false;
            return true;
        }

        public List<Image> GetAllImage()
        {
           var result = _dbContext.Connection.Query<Image>("IMAGE_PACKAGE.GETALLIMAGE", commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

        public Image GetImageByHall(int Hallid)
        {
            var p = new DynamicParameters();
            p.Add("Hall", Hallid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = _dbContext.Connection.QueryFirstOrDefault<Image>("IMAGE_PACKAGE.GetImageByHallId", p, commandType: CommandType.StoredProcedure);
            if (result == null)
                return null;
            return result;
        }

        public Image GetImageById(int ImageId)
        {
            var p = new DynamicParameters();
            p.Add("id", ImageId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = _dbContext.Connection.QueryFirstOrDefault<Image>("IMAGE_PACKAGE.GetImageByid", p, commandType: CommandType.StoredProcedure);
            if (result == null)
                return null;
            return result;
        }

        public Image GetImageByUrl(string Url)
        {
            var p = new DynamicParameters();
            p.Add("url", Url, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = _dbContext.Connection.QueryFirstOrDefault<Image>("IMAGE_PACKAGE.GetImageByUrl", p, commandType: CommandType.StoredProcedure);
            if (result == null)
                return null;
            return result;
        }

        public bool UpdateImage(ImageToUpdateDto imageToUpdateDto)
        {
            var p = new DynamicParameters();
            p.Add("Id", imageToUpdateDto.ImageId, dbType: DbType.Int32, direction: ParameterDirection.Input);

            p.Add("url", imageToUpdateDto.Imageurl, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Descp", imageToUpdateDto.Description, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Hall", imageToUpdateDto.Hallid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("pid", imageToUpdateDto.Publicid, dbType: DbType.String, direction: ParameterDirection.Input);

            var result = _dbContext.Connection.ExecuteAsync("IMAGE_PACKAGE.UpdateImage", p, commandType: CommandType.StoredProcedure);
            if (result == null)
                return false;
            return true;
        }
    }
}
