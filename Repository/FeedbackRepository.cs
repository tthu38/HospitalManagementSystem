using System.Collections.Generic;
using Business;
using DataAccess;

namespace Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly FeedbackDAO dao = new();

        public void Add(Feedback feedback) => dao.Add(feedback);
        public List<Feedback> GetByDoctor(int id) => dao.GetByDoctor(id);
        public List<Feedback> GetAll() => dao.GetAll();
    }
}
