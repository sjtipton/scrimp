using scrimp.Entities;
using System;
using System.Collections.Generic;

namespace scrimp.Services
{
    public class AccountService : IAccountService
    {
        private DataContext _context;

        public AccountService(DataContext context)
        {
            _context = context;
        }

        public Account CreateUserAccount(int id, Account account)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Account GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetUserAccounts(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Account account)
        {
            throw new NotImplementedException();
        }
    }
}
