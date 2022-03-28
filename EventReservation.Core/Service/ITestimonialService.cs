using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.Service
{
    public interface ITestimonialService
    {
        List<Testimonial> GetAllTestimonial();
        List<Testimonial> GetAllTestimonialApproved();
        bool CreateTestimonial(ToAddTestimonial toAddTestimonial);
        bool DeleteTestimonial(int id);
        Testimonial GetTestimonialById(int id);
        bool ApproveTestimonial(int id);
        bool UnapproveTestimonial(int id);
    }
}
