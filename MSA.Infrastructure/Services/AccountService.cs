using MSA.Domain.Entities;
using MSA.Infrastructure.Migrations;
using Repositories;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Add(Account account)
        {
            _accountRepository.Add(account);
        }

        public void Delete(Account account)
        {
            _accountRepository.Delete(account);
        }

        public Account? GetAccountByUsernameAndPassword(string username, string password)
        {
            try
            {
                return _accountRepository.GetAll()
                    .FirstOrDefault(x => x.FullName!.Equals(username) && x.Password!.Equals(password));
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IEnumerable<Account> SearchByName(string name)
        {
            return _accountRepository.GetAll().Where(x => x.FullName!.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public IEnumerable<Account> GetAll()
        {
            return _accountRepository.GetAll();
        }

        public Account? GetById(Guid id)
        {
            return _accountRepository.GetById(id);
        }

        public void Save()
        {
            _accountRepository.Save();
        }

        public void Update(Account account)
        {
            _accountRepository.Update(account);
        }
    }
}
