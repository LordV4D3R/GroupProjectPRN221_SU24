using MSA.Application.DAO;
using MSA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Infrastructure.DAOs
{
    public class VoucherDAO:BaseDAO<Voucher>
    {
        private static VoucherDAO instance = null;
        private static readonly object lockObject = new();

        public VoucherDAO()
        {
        }
        public static VoucherDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance != null)
                    {
                        return instance;
                    }
                    instance = new VoucherDAO();
                    return instance;
                }
            }
        }
    }
}
