using Microsoft.AspNetCore.Mvc;
using scrimp.Dtos;
using System;

namespace scrimp.Controllers
{
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        // TODO implement DI
        public TransactionsController() { }

        // GET transaction_accounts/:id/transactions
        [HttpGet("transaction_accounts/:id/transactions")]
        public IActionResult GetTransactionAccountTransactions(int id)
        {
            throw new NotImplementedException();
        }

        // GET accounts/:id/transactions
        [HttpGet("accounts/:id/transactions")]
        public IActionResult GetAccountTransactions(int id)
        {
            throw new NotImplementedException();
        }

        // GET users/:id/transactions
        [HttpGet("users/:id/transactions")]
        public IActionResult GetUserTransactions(int id)
        {
            throw new NotImplementedException();
        }

        // POST transaction_accounts/:id/transactions
        [HttpPost("transaction_accounts/:id/transactions")]
        public IActionResult CreateTransactionAccountTransaction(int id, [FromBody]TransactionDto transactionDto)
        {
            throw new NotImplementedException();
        }

        // GET transactions/:id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            throw new NotImplementedException();
        }

        // PUT transactions/:id
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]TransactionDto transactionDto)
        {
            throw new NotImplementedException();
        }
    }
}
