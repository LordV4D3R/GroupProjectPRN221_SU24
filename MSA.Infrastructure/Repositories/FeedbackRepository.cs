using MSA.Domain.Entities;
using MSA.Infrastructure.DAOs;
using MSA.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Infrastructure.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        public void Add(Feedback feedback) => FeedbackDAO.Instance.Add(feedback);

        public void Delete(Feedback feedback) => FeedbackDAO.Instance.Delete(feedback);

        public IEnumerable<Feedback> GetAll() => FeedbackDAO.Instance.GetAll();

        public Feedback? GetById(Guid id) => FeedbackDAO.Instance.GetById(id);

        public void Save() => FeedbackDAO.Instance.Save();

    }
}
