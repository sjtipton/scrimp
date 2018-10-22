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
    public class TransactionsController : AuthorizeController
    {
        private IUserService _userService;
        private IAccountService _accountService;
        private ITransactionAccountService _transactionAccountService;
        private ITransactionService _transactionService;
        private IErrorService _errorService;
        private IMapper _mapper;

        public TransactionsController(
            IUserService userService,
            IAccountService accountService,
            ITransactionAccountService transactionAccountService,
            ITransactionService transactionService,
            IErrorService errorService,
            IMapper mapper)
        {
            _userService = userService;
            _accountService = accountService;
            _transactionAccountService = transactionAccountService;
            _transactionService = transactionService;
            _errorService = errorService;
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
                return NotFound(_errorService.NotFound("transaction account", id, HttpContext.Request));
            }

            if (transactionAccount is TransactionAccount)
            {
                var transactionAccountTransactions = _transactionService.GetTransactionAccountTransactions(id);
                var transactionAccountTransactionDtos = _mapper.Map<IEnumerable<TransactionDto>>(transactionAccountTransactions);
                return Ok(transactionAccountTransactionDtos);
            }
            return BadRequest(_errorService.BadRequest("transaction account", id, HttpContext.Request));
        }

        // GET api/accounts/:id/transactions
        [HttpGet]
        [Route("accounts/{id}/transactions")]
        public IActionResult GetAccountTransactions(int id)
        {
            var account = _accountService.GetById(id);

            if (account == null)
            {
                return NotFound(_errorService.NotFound("account", id, HttpContext.Request));
            }

            if (account is Account)
            {
                var accountTransactions = _transactionService.GetAccountTransactions(id);
                var accountTransactionDtos = _mapper.Map<IEnumerable<TransactionDto>>(accountTransactions);
                return Ok(accountTransactionDtos);
            }
            return BadRequest(_errorService.BadRequest("account", id, HttpContext.Request));
        }

        // GET api/users/:id/transactions
        [HttpGet]
        [Route("users/{id}/transactions")]
        public IActionResult GetUserTransactions(int id)
        {
            var user = _userService.GetById(id);

            if (user == null)
            {
                return NotFound(_errorService.NotFound("user", id, HttpContext.Request));
            }

            if (user is User)
            {
                var userTransactions = _transactionService.GetUserTransactions(id);
                var userTransactionDtos = _mapper.Map<IEnumerable<TransactionDto>>(userTransactions);
                return Ok(userTransactionDtos);
            }
            return BadRequest(_errorService.BadRequest("user", id, HttpContext.Request));
        }

        // POST api/transaction_accounts/:id/transactions
        [HttpPost]
        [Route("transaction_accounts/{id}/transactions")]
        public IActionResult CreateTransactionAccountTransaction(int id, [FromBody]TransactionDto transactionDto)
        {
            var transactionAccount = _transactionAccountService.GetById(id);

            if (transactionAccount == null)
            {
                return NotFound(_errorService.NotFound("transaction account", id, HttpContext.Request));
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
                    return BadRequest(_errorService.BadRequest(ex, HttpContext.Request));
                }
            }
            return BadRequest(_errorService.BadRequest("transaction account", id, HttpContext.Request));
        }

        // GET api/transactions/:id
        [HttpGet("transactions/{id}")]
        public IActionResult GetById(int id)
        {
            var transaction = _transactionService.GetById(id);

            if (transaction == null)
            {
                return NotFound(_errorService.NotFound("transaction", id, HttpContext.Request));
            }

            if (transaction is Transaction)
            {
                var transactionDto = _mapper.Map<TransactionDto>(transaction);
                return Ok(transactionDto);
            }
            return BadRequest(_errorService.BadRequest("transaction", id, HttpContext.Request));
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
                return BadRequest(_errorService.BadRequest(ex, HttpContext.Request));
            }
        }
    }
}
