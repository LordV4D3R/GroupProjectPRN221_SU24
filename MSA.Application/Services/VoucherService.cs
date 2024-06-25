using MSA.Application.IRepositories;
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
        private readonly IVoucherRepository _voucherRepository;

        public VoucherService(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public void Add(Voucher voucher)
        {
            _voucherRepository.Add(voucher);
        }

        public void Delete(Voucher voucher)
        {
            _voucherRepository.Delete(voucher);
        }

        public IEnumerable<Voucher> GetAll()
        {
            return _voucherRepository.GetAll();
        }

        public Voucher? GetById(Guid id)
        {
            return _voucherRepository.GetById(id);
        }

        public void Save()
        {
            _voucherRepository.Save();
        }

        public IEnumerable<Voucher> SearchByName(string name)
        {
            return _voucherRepository.GetAll().Where(x => x.VoucherName!.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void Update(Voucher voucher)
        {
            _voucherRepository.Update(voucher);
        }
    }
}
