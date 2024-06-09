using MSA.Domain.Entities;
using MSA.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherService _voucherService;

        public VoucherService(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        public void Add(Voucher voucher)
        {
            _voucherService.Add(voucher);
        }

        public void Delete(Voucher voucher)
        {
            _voucherService.Delete(voucher);
        }

        public IEnumerable<Voucher> GetAll()
        {
            return _voucherService.GetAll();
        }

        public Voucher? GetById(Guid id)
        {
            return _voucherService.GetById(id);
        }

        public void Save()
        {
            _voucherService.Save();
        }

        public IEnumerable<Voucher> SearchByName(string name)
        {
            return _voucherService.GetAll().Where(x => x.VoucherName!.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void Update(Voucher voucher)
        {
            _voucherService.Update(voucher);
        }
    }
}
