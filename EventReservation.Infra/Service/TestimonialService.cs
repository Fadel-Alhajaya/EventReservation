using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using EventReservation.Core.Repository;
using EventReservation.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Infra.Service
{
   public class TestimonialService: ITestimonialService
    {
        private readonly ITestimonialRepository _testimonialRepository;
        public TestimonialService(ITestimonialRepository testimonialRepository)
        {
            _testimonialRepository = testimonialRepository;
        }

        public bool ApproveTestimonial(int id)
        {
            return _testimonialRepository.ApproveTestimonial(id);
        }

        public bool CreateTestimonial(ToAddTestimonial toAddTestimonial)
        {
            return _testimonialRepository.CreateTestimonial(toAddTestimonial);
        }

        public bool DeleteTestimonial(int id)
        {
            return _testimonialRepository.DeleteTestimonial(id);
        }

        public List<Testimonial> GetAllTestimonial()
        {
            return _testimonialRepository.GetAllTestimonial();
        }

        public Testimonial GetTestimonialById(int id)
        {
            return _testimonialRepository.GetTestimonialById(id);
        }

        public bool UnapproveTestimonial(int id)
        {
            return _testimonialRepository.UnapproveTestimonial(id);
        }
    }
}
