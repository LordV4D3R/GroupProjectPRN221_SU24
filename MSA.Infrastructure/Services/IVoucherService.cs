using MSA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Infrastructure.Services
{
    public interface IVoucherService
    {
        IEnumerable<Voucher> SearchByName(string name);
        IEnumerable<Voucher> GetAll();
        Voucher? GetById(Guid id);
        void Add(Voucher voucher);
        void Update(Voucher voucher);
        void Delete(Voucher voucher);
        void Save();
    }
}
