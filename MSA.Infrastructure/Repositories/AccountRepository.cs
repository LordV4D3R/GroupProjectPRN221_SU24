using MSA.Application.DAO;
using MSA.Domain.Entities;
using MSA.Application.IRepositories;

namespace MSA.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public void Add(Account account) => AccountDAO.Instance.Add(account);

        public void Delete(Account account) => AccountDAO.Instance.Delete(account);

        public IEnumerable<Account> GetAll() => AccountDAO.Instance.GetAll();

        public Account? GetById(Guid id) => AccountDAO.Instance.GetById(id);

        public void Save() => AccountDAO.Instance.Save();

        public void Update(Account account) => AccountDAO.Instance.Update(account);
        public void Update2(Account account) => AccountDAO.Instance.Update2(account, GetById(account.Id));
    }
}
