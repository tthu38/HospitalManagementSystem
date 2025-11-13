using System.Collections.Generic;
using System.Linq;
using Business;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class FeedbackDAO
    {
        public void Add(Feedback feedback)
        {
            using var context = new HospitalManagementContext();
            context.Feedbacks.Add(feedback);
            context.SaveChanges();
        }

        public List<Feedback> GetByDoctor(int doctorId)
        {
            using var context = new HospitalManagementContext();
            return context.Feedbacks.Include(f => f.Patient)
                .Where(f => f.DoctorId == doctorId).ToList();
        }

        public List<Feedback> GetAll()
        {
            using var context = new HospitalManagementContext();
            return context.Feedbacks.Include(f => f.Doctor).Include(f => f.Patient).ToList();
        }
    }
}
