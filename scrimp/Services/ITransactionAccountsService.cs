using scrimp.Entities;
using System.Collections.Generic;

namespace scrimp.Services
{
    public interface ITransactionAccountsService
    {
        IEnumerable<TransactionAccount> GetUserTransactionAccounts(int id);
        TransactionAccount GetById(int id);
    }
}
