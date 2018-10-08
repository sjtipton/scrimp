using scrimp.Entities;
using System.Collections.Generic;

namespace scrimp.Services
{
    public interface IAccountService
    {
        IEnumerable<Account> GetUserAccounts(int id);
        Account GetById(int id);
        Account CreateUserAccount(int id, Account account);
        void Update(Account account);
        void Delete(int id);
    }
}
