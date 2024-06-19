using MSA.Domain.Dtos.Account;
using MSA.Domain.Entities;

namespace MSA.Application.IRepositories
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAll();
        IEnumerable<AccountViewModel> GetAllViewModel();
        Account? GetById(Guid id);
        void Add(Account account);
        void Update(Account account);
        void Update2(Account account);
        void Delete(Account account);
        void Save();
    }
}
