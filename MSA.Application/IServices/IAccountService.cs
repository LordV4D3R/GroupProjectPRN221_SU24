using MSA.Domain.Entities;

namespace MSA.Application.IServices
{
    public interface IAccountService
    {
        Account? GetAccountByUsernameAndPassword(string username, string password);
        IEnumerable<Account> SearchByName(string name);
        IEnumerable<Account> GetAll();
        Account? GetById(Guid id);
        void Add(Account account);
        void Update(Account account);
        void Delete(Account account);
        void Save();
    }
}
