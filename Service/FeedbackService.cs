using System.Collections.Generic;
using Business;
using Repository;

namespace Service
{
    public class FeedbackService
    {
        private readonly IFeedbackRepository repo;

        public FeedbackService()
        {
            repo = new FeedbackRepository();
        }

        public void Create(Feedback feedback)
        {
            repo.Add(feedback);
        }

        public List<Feedback> GetByDoctor(int doctorId)
        {
            return repo.GetByDoctor(doctorId);
        }

        public List<Feedback> GetAll()
        {
            return repo.GetAll();
        }
    }
}
