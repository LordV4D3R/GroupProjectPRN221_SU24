using MSA.Domain.Entities;

namespace MSA.Infrastructure.IRepositories
{
    public interface IVoucherRepository
    {
        IEnumerable<Voucher> GetAll();
        Voucher GetById(Guid id);
        void Add(Voucher voucher);
        void Update(Voucher voucher);
        void Delete(Voucher voucher);
        void Save();
    }
}
