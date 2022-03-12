using Dapper;
using EventReservation.Core.Common;
using EventReservation.Core.Data;
using EventReservation.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EventReservation.Infra.Repository
{
   public  class ReviewRepository: IReviewRepository
    {

        private readonly IDbContext DbContext;
        public ReviewRepository(IDbContext _DbContext)
        {
            DbContext = _DbContext;
        }






        public bool AddReview(Review review)
        {
            var p = new DynamicParameters();
            p.Add("Ra", review.Rate, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            p.Add("WEBID", review.WebsiteId, dbType: DbType.Int32, direction: ParameterDirection.Input);

            var result = DbContext.Connection.ExecuteAsync("REVIEW_F_PACKAGE.CREATETREVIEW", p, commandType: CommandType.StoredProcedure);
            if (result == null) return false;
            return true;
        }

        public List<Review> GetAllReviews()
        {
            IEnumerable<Review> result = DbContext.Connection.Query<Review>("REVIEW_F_PACKAGE.GETALLREVIEW", commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

        //public List<AvgRateDTO> GetAvgRate()
        //{
        //    IEnumerable<AvgRateDTO> result = DbContext.Connection.Query<AvgRateDTO>("REVIEW_F_PACKAGE.GETAVGREVIEW", commandType: CommandType.StoredProcedure);

        //    return result.ToList();
        //}
    
}
}
