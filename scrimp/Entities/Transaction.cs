using scrimp.Domain;
using System;
using System.Collections.Generic;

namespace scrimp.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public string CheckNumber { get; set; }
        public TransactionType Type { get; set; }
        public string Memo { get; set; }
        public string Payee { get; set; }
        public decimal Amount { get; set; }
        public DateTime AnticipatedDate { get; set; }
        public bool IsTransfer { get; set; }
        public Category Category { get; set; }
        public string Note { get; set; }
        public IEnumerable<string> Labels { get; set; }
        public TransactionAccount TransactionAccount { get; set; }
        public TransactionStatus Status { get; set; }
        public decimal ClosingBalance { get; set; }
    }
}
