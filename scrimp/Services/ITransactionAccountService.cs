using scrimp.Entities;
using System.Collections.Generic;

namespace scrimp.Services
{
    public interface ITransactionAccountService
    {
        IEnumerable<TransactionAccount> GetUserTransactionAccounts(int id);
        TransactionAccount GetById(int id);
    }
}
