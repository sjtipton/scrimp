using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using scrimp.Dtos;
using scrimp.Entities;
using scrimp.Helpers;
using scrimp.Services;
using System.Collections.Generic;

namespace scrimp.Controllers
{
    [ApiController]
    [Route("api")]
    public class TransactionsController : ControllerBase
    {
        private IUserService _userService;
        private IAccountService _accountService;
        private ITransactionAccountService _transactionAccountService;
        private ITransactionService _transactionService;
        private IMapper _mapper;

        public TransactionsController(IUserService userService, IAccountService accountService, ITransactionAccountService transactionAccountService, ITransactionService transactionService, IMapper mapper)
        {
            _userService = userService;
            _accountService = accountService;
            _transactionAccountService = transactionAccountService;
            _transactionService = transactionService;
            _mapper = mapper;
        }

        // GET api/transaction_accounts/:id/transactions
        [HttpGet]
        [Route("transaction_accounts/{id}/transactions")]
        public IActionResult GetTransactionAccountTransactions(int id)
        {
            var transactionAccount = _transactionAccountService.GetById(id);

            if (transactionAccount == null)
            {
                return NotFound();
            }

            if (transactionAccount is TransactionAccount)
            {
                var transactionAccountTransactions = _transactionService.GetTransactionAccountTransactions(id);
                var transactionAccountTransactionDtos = _mapper.Map<IEnumerable<TransactionDto>>(transactionAccountTransactions);
                return Ok(transactionAccountTransactionDtos);
            }
            return BadRequest("The transaction account is not valid.");
        }

        // GET api/accounts/:id/transactions
        [HttpGet]
        [Route("accounts/{id}/transactions")]
        public IActionResult GetAccountTransactions(int id)
        {
            var account = _accountService.GetById(id);

            if (account == null)
            {
                return NotFound();
            }

            if (account is Account)
            {
                var accountTransactions = _transactionService.GetAccountTransactions(id);
                var accountTransactionDtos = _mapper.Map<IEnumerable<TransactionDto>>(accountTransactions);
                return Ok(accountTransactionDtos);
            }
            return BadRequest("The account is not valid.");
        }

        // GET api/users/:id/transactions
        [HttpGet]
        [Route("users/{id}/transactions")]
        public IActionResult GetUserTransactions(int id)
        {
            var user = _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            if (user is User)
            {
                var userTransactions = _transactionService.GetUserTransactions(id);
                var userTransactionDtos = _mapper.Map<IEnumerable<TransactionDto>>(userTransactions);
                return Ok(userTransactionDtos);
            }
            return BadRequest("The user is not valid.");
        }

        // POST api/transaction_accounts/:id/transactions
        [HttpPost]
        [Route("transaction_accounts/{id}/transactions")]
        public IActionResult CreateTransactionAccountTransaction(int id, [FromBody]TransactionDto transactionDto)
        {
            var transactionAccount = _transactionAccountService.GetById(id);

            if (transactionAccount == null)
            {
                return NotFound();
            }

            if (transactionAccount is TransactionAccount)
            {
                var transaction = _mapper.Map<Transaction>(transactionDto);

                try
                {
                    _transactionService.CreateTransactionAccountTransaction(id, transaction);
                    return Ok();
                }
                catch (AppException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
            return BadRequest("The transaction account is not valid.");
        }

        // GET api/transactions/:id
        [HttpGet("transactions/{id}")]
        public IActionResult GetById(int id)
        {
            var transaction = _transactionService.GetById(id);
            var transactionDto = _mapper.Map<TransactionDto>(transaction);
            return Ok(transactionDto);
        }

        // PUT api/transactions/:id
        [HttpPut("transactions/{id}")]
        public IActionResult Update(int id, [FromBody]TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);
            transaction.Id = id;

            try
            {
                _transactionService.Update(transaction);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
