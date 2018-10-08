using System;
using System.Collections.Generic;
using scrimp.Entities;

namespace scrimp.Services
{
    public class TransactionService : ITransactionService
    {
        private DataContext _context;

        public TransactionService(DataContext context)
        {
            _context = context;
        }

        public Transaction CreateTransactionAccountTransaction(int id, Transaction account)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetAccountTransactions(int id)
        {
            throw new NotImplementedException();
        }

        public Transaction GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetTransactionAccountTransactions(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetUserTransactions(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Transaction category)
        {
            throw new NotImplementedException();
        }
    }
}
