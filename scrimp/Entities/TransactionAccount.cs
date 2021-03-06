﻿using scrimp.Domain;
using scrimp.Helpers.Timestamps;
using System;

namespace scrimp.Entities
{
    public class TransactionAccount : IAuditableModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public decimal CurrentBalance { get; set; }
        public DateTime CurrentBalanceDate { get; set; }
        public decimal StartingBalance { get; set; }
        public DateTime StartingBalanceDate { get; set; }
        public string CurrencyCode { get; set; }
        public TransactionStatus Status { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
