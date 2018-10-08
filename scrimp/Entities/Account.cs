using scrimp.Domain;
using System;
using System.Collections.Generic;

namespace scrimp.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string CurrencyCode { get; set; }
        public AccountType Type { get; set; }
        public bool IsNetWorth { get; set; }
        public IEnumerable<TransactionAccount> TransactionAccounts { get; set; }
        public decimal CurrentBalance { get; set; }
        public DateTime CurrentBalanceDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
