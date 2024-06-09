using MSA.Application.DAO;
using MSA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Infrastructure.DAOs
{
    public class FeedbackDAO : BaseDAO<Feedback>
    {
        private static FeedbackDAO instance;
        private static readonly object lockObject = null;
        public FeedbackDAO() { }
        public static FeedbackDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance != null)
                    {
                        return instance;
                    }
                    instance = new FeedbackDAO();
                    return instance;
                }
            }
        }
    }
}
