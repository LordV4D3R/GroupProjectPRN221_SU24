using MSA.Application.DAO;
using MSA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Infrastructure.DAOs
{
    public class CategoryDAO: BaseDAO<Category>
    {
        private static CategoryDAO instance = null;
        private static readonly object lockObject = new();

        public CategoryDAO()
        {
        }
        public static CategoryDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance != null)
                    {
                        return instance;
                    }
                    instance = new CategoryDAO();
                    return instance;
                }
            }
        }
    }
}
