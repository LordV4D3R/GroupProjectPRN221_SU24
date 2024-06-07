﻿using MSA.Domain.Entities;

namespace Repositories
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAll();
        Account? GetById(Guid id);
        void Add(Account account);
        void Update(Account account);
        void Delete(Account account);
        void Save();
    }
}
