using MSA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Application.DAO
{
    public class AccountDAO : BaseDAO<Account>
    {
        private static AccountDAO instance = null;
        private static readonly object lockObject = new();
        public AccountDAO() { }
        public static AccountDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance != null)
                    {
                        return instance;
                    }
                    instance = new AccountDAO();
                    return instance;
                }
            }
        }
    }
}
