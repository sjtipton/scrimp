using scrimp.Entities;
using System.Collections.Generic;

namespace scrimp.Services
{
    public interface IAccountsService
    {
        IEnumerable<Account> GetUserAccounts(int id);
        Account GetById(int id);
        Account CreateUserAccount(int id, Account account);
        void Update(int id, Account account);
        void Delete(int id);
    }
}
