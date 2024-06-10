using MSA.Domain.Entities;
using MSA.Infrastructure.DAOs;
using MSA.Infrastructure.IRepositories;


namespace Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        public void Add(Voucher voucher) => VoucherDAO.Instance.Add(voucher);

        public void Delete(Voucher voucher) => VoucherDAO.Instance.Delete(voucher);

        public IEnumerable<Voucher> GetAll() => VoucherDAO.Instance.GetAll();

        public Voucher GetById(Guid id) => VoucherDAO.Instance.GetById(id);

        public void Save() => VoucherDAO.Instance.Save();

        public void Update(Voucher voucher) => VoucherDAO.Instance.Update(voucher);
    }
}
