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
    public class TestimonialRepository: ITestimonialRepository
    {
        private readonly IDbContext _dbContext;

        public TestimonialRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public bool CreateTestimonial(ToAddTestimonial toAddTestimonial)
        {
            var parmeter = new DynamicParameters();

            parmeter.Add("NAME", toAddTestimonial.Personalname, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("IMG", toAddTestimonial.ImageUrl, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("FEEDB", toAddTestimonial.Feedback, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("STS", toAddTestimonial.Status, dbType: DbType.String, direction: ParameterDirection.Input);
            parmeter.Add("WEBID", toAddTestimonial.Websiteid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parmeter.Add("PID", toAddTestimonial.Publicid, dbType: DbType.String, direction: ParameterDirection.Input);


            var result = _dbContext.Connection.ExecuteAsync("TESTIMONIAL_F_PACKAGE.CREATETESTIMONIAL", parmeter, commandType: CommandType.StoredProcedure);
            if (result == null)
                return false;

            return true;
        }

        public bool DeleteTestimonial(int id)
        {
            var parmeter = new DynamicParameters();
            parmeter.Add("ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            _dbContext.Connection.ExecuteAsync("TESTIMONIAL_F_PACKAGE.DELETETESTIMONIAL", parmeter, commandType: CommandType.StoredProcedure);

            return true;
        }

        public List<Testimonial> GetAllTestimonial()
        {
            IEnumerable<Testimonial> result = _dbContext.Connection.Query<Testimonial>("TESTIMONIAL_F_PACKAGE.GETALLTESTIMONIAL", commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

        public Testimonial GetTestimonialById(int id)
        {
            var parmeter = new DynamicParameters();
            parmeter.Add("ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            IEnumerable<Testimonial> result = _dbContext.Connection.Query<Testimonial>("TESTIMONIAL_F_PACKAGE.GETTESTIMONIALBYID", parmeter, commandType: CommandType.StoredProcedure);


            return result.FirstOrDefault();
        }

        public bool ApproveTestimonial(int id)
        {
            var parmeter = new DynamicParameters();
            parmeter.Add("ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            _dbContext.Connection.ExecuteAsync("TESTIMONIAL_F_PACKAGE.APPROVETESTIMONIAL", parmeter, commandType: CommandType.StoredProcedure);

            return true;
        }

        public bool UnapproveTestimonial(int id)
        {
            var parmeter = new DynamicParameters();
            parmeter.Add("ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            _dbContext.Connection.ExecuteAsync("TESTIMONIAL_F_PACKAGE.UNAPPROVETESTIMONIAL", parmeter, commandType: CommandType.StoredProcedure);

            return true;
        }
    }
}
