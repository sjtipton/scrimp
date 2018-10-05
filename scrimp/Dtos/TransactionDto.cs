using scrimp.Entities;
using System;
using System.Collections.Generic;

namespace scrimp.Dtos
{
    public class TransactionDto
    {
        public string Payee { get; set; }
        public decimal Amount { get; set; }
        public DateTime AnticipatedDate { get; set; }
        public bool IsTransfer { get; set; }
        public IEnumerable<TransactionLabel> Labels { get; set; }
        public Category Category { get; set; }
        public string Note { get; set; }
        public string Memo { get; set; }
        public string CheckNumber { get; set; }
    }
}
