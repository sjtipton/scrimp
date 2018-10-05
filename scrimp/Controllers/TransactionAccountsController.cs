using Microsoft.AspNetCore.Mvc;
using System;

namespace scrimp.Controllers
{
    [ApiController]
    public class TransactionAccountsController : ControllerBase
    {
        // TODO implement DI
        public TransactionAccountsController() { }

        // GET users/:id/transaction_accounts
        [HttpGet("users/:id/transaction_accounts")]
        public IActionResult GetUserTransactionAccounts(int id)
        {
            throw new NotImplementedException();
        }

        // GET transaction_accounts/:id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
