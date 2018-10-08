using scrimp.Entities;
using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }

        public IEnumerable<TransactionAccount> GetUserTransactionAccounts(int id)
        {
            throw new NotImplementedException();
        }
    }
}
