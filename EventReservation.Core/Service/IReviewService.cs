using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.Service
{
    public interface IReviewService
    {
        List<Review> GetAllReviews();
        //Update Update()

        bool AddReview(Review review);

        //List<AvgRateDTO> GetAvgRate();
    }
}
