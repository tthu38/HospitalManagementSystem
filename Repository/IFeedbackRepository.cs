using System.Collections.Generic;
using Business;

namespace Repository
{
    public interface IFeedbackRepository
    {
        void Add(Feedback feedback);
        List<Feedback> GetByDoctor(int doctorId);
        List<Feedback> GetAll();
    }
}
