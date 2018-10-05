using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using scrimp.Services;
using System;

namespace scrimp.Controllers
{
    [ApiController]
    public class TransactionAccountsController : ControllerBase
    {
        private IUserService _userService;
        private ITransactionAccountService _transactionAccountService;
        private IMapper _mapper;

        public TransactionAccountsController(IUserService userService, ITransactionAccountService transactionAccountService, IMapper mapper)
        {
            _userService = userService;
            _transactionAccountService = transactionAccountService;
            _mapper = mapper;
        }

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
