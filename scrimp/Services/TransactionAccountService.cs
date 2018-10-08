using scrimp.Entities;
using System.Collections.Generic;
using System.Linq;

namespace scrimp.Services
{
    public class TransactionAccountService : ITransactionAccountService
    {
        private DataContext _context;

        public TransactionAccountService(DataContext context)
        {
            _context = context;
        }

        public TransactionAccount GetById(int id)
        {
            return _context.TransactionAccounts.Find(id);
        }

        public IEnumerable<TransactionAccount> GetUserTransactionAccounts(int userId)
        {
            return _context.TransactionAccounts.Where(x => x.UserId == userId).ToList();
        }
    }
}
