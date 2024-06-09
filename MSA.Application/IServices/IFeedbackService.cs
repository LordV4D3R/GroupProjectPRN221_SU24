using MSA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Application.IServices
{
    public interface IFeedbackService
    {
        IEnumerable<Feedback> GetAll();
        Feedback? GetById(Guid id);
        void Add(Feedback feedback);
        void Delete(Feedback feedback);
        void Save();
    }
}
