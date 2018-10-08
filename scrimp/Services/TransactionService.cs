using System.Collections.Generic;
using System.Linq;
using scrimp.Entities;
using scrimp.Helpers;

namespace scrimp.Services
{
    public class TransactionService : ITransactionService
    {
        private DataContext _context;

        public TransactionService(DataContext context)
        {
            _context = context;
        }

        public Transaction CreateTransactionAccountTransaction(int transactionAccountId, Transaction transaction)
        {
            var transactionAccount = _context.TransactionAccounts.Find(transactionAccountId);
            transaction.TransactionAccount = transactionAccount ?? throw new AppException("Transaction Account not found. Cannot create an Transaction.");

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return transaction;
        }

        public IEnumerable<Transaction> GetAccountTransactions(int accountId)
        {
            return _context.Transactions.Where(x => x.AccountId == accountId).ToList();
        }

        public Transaction GetById(int id)
        {
            return _context.Transactions.Find(id);
        }

        public IEnumerable<Transaction> GetTransactionAccountTransactions(int transactionAccountId)
        {
            return _context.Transactions.Where(x => x.TransactionAccountId == transactionAccountId).ToList();
        }

        public IEnumerable<Transaction> GetUserTransactions(int userId)
        {
            return _context.Transactions.Where(x => x.UserId == userId).ToList();
        }

        public void Update(Transaction transactionParam)
        {
            var transaction = _context.Transactions.Find(transactionParam.Id);

            if (transaction == null)
                throw new AppException("Transaction not found. Cannot update a Transaction.");

            transaction.Payee = transactionParam.Payee;
            transaction.Amount = transactionParam.Amount;
            transaction.AnticipatedDate = transactionParam.AnticipatedDate;
            transaction.IsTransfer = transactionParam.IsTransfer;
            transaction.Labels = transactionParam.Labels;
            transaction.Category = transactionParam.Category;
            transaction.Note = transactionParam.Note;
            transaction.Memo = transactionParam.Memo;
            transaction.CheckNumber = transactionParam.CheckNumber;

            _context.Transactions.Update(transaction);
            _context.SaveChanges();
        }
    }
}
