using MSA.Domain.Dtos.Account;
using MSA.Domain.Entities;

namespace MSA.Application.IServices
{
    public interface IAccountService
    {
        Account? GetAccountByUsernameAndPassword(AccountLoginDto accountLoginDto);
        IEnumerable<Account> SearchByName(string name);
        IEnumerable<Account> GetAll();
        Account? GetById(Guid id);
        void Add(Account account);
        void Update(Account account);
        void Delete(Account account);
        void Save();
    }
}
