using scrimp.Entities;
using scrimp.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace scrimp.Services
{
    public class AccountService : IAccountService
    {
        private DataContext _context;

        public AccountService(DataContext context)
        {
            _context = context;
        }

        public Account CreateUserAccount(int userId, Account account)
        {
            var user = _context.Users.Find(userId);

            if (user == null)
                throw new AppException("User not found. Cannot create an Account.");

            account.UserId = user.Id;

            _context.Accounts.Add(account);
            _context.SaveChanges();

            return account;
        }

        public void Delete(int id)
        {
            var account = _context.Accounts.Find(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                _context.SaveChanges();
            }
        }

        public Account GetById(int id)
        {
            return _context.Accounts.Find(id);
        }

        public IEnumerable<Account> GetUserAccounts(int userId)
        {
            return _context.Accounts.Where(x => x.UserId == userId).ToList();
        }

        public void Update(int userId, Account accountParam)
        {
            var user = _context.Users.Find(userId);

            if (user == null)
                throw new AppException("User not found. Cannot update an Account.");

            var account = _context.Accounts.Find(accountParam.Id);

            if (account == null)
                throw new AppException("Account not found. Cannot update an Account.");

            account.Name = accountParam.Name;
            account.CurrencyCode = accountParam.CurrencyCode;
            account.Type = accountParam.Type;
            account.IsNetWorth = accountParam.IsNetWorth;

            _context.Accounts.Update(account);
            _context.SaveChanges();
        }
    }
}
