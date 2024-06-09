using MSA.Application.DAO;
using MSA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Infrastructure.DAOs
{
    public class BatchDAO : BaseDAO<Batch>
    {
        private static BatchDAO instance = null;
        private static readonly object lockObject = new();
        public BatchDAO() { }
        public static BatchDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance != null)
                    {
                        return instance;
                    }
                    instance = new BatchDAO();
                    return instance;
                }
            }
        }
    }
}
