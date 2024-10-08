﻿using MSA.Application.IServices;
using MSA.Domain.Entities;
using MSA.Application.IRepositories;
using MSA.Domain.Dtos.Account;

namespace MSA.Application.Services
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

        public Account? GetAccountByUsernameAndPassword(AccountLoginDto accountLoginDto)
        {
            try
            {
                return _accountRepository.GetAll()
                    .FirstOrDefault(x => x.Username!.Equals(accountLoginDto.Username) && x.Password!.Equals(accountLoginDto.Password));
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IEnumerable<Account> SearchByName(string name)
        {
            return _accountRepository.GetAll().Where(x => x.FullName!.Contains(name, StringComparison.OrdinalIgnoreCase) && x.IsDeleted == false).ToList();
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
        public void Update2(Account account)
        {
            _accountRepository.Update2(account);
        }
    }
}
