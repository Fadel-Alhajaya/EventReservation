using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using EventReservation.Core.Repository;
using EventReservation.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Infra.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public bool AddReview(Review review)
        {
            return _reviewRepository.AddReview(review);
        }

        public List<Review> GetAllReviews()
        {
            return _reviewRepository.GetAllReviews();
        }

        //public List<AvgRateDTO> GetAvgRate()
        //{
        //    return _reviewRepository.GetAvgRate();
        //}
    }
}
