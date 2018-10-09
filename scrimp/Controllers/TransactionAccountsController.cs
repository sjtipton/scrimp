using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using scrimp.Entities;
using scrimp.Dtos;
using scrimp.Services;
using System.Collections.Generic;

namespace scrimp.Controllers
{
    [ApiController]
    [Route("api")]
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

        // GET api/users/:id/transaction_accounts
        [HttpGet]
        [Route("users/{id}/transaction_accounts")]
        public IActionResult GetUserTransactionAccounts(int id)
        {
            var user = _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            if (user is User)
            {
                var userTransactionAccounts = _transactionAccountService.GetUserTransactionAccounts(id);
                var userTransactionAccountDtos = _mapper.Map<IEnumerable<TransactionAccountDto>>(userTransactionAccounts);
                return Ok(userTransactionAccountDtos);
            }
            return BadRequest("The user is not valid.");
        }

        // GET api/transaction_accounts/:id
        [HttpGet("transaction_accounts/{id}")]
        public IActionResult GetById(int id)
        {
            var transactionAccount = _transactionAccountService.GetById(id);
            var transactionAccountDto = _mapper.Map<TransactionAccountDto>(transactionAccount);
            return Ok(transactionAccountDto);
        }
    }
}
