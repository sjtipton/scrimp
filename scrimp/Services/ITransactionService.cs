using scrimp.Entities;
using System.Collections.Generic;

namespace scrimp.Services
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetTransactionAccountTransactions(int id);
        IEnumerable<Transaction> GetAccountTransactions(int id);
        IEnumerable<Transaction> GetUserTransactions(int id);
        Transaction GetById(int id);
        Transaction CreateTransactionAccountTransaction(int id, Transaction account);
        void Update(Transaction transaction);
    }
}
