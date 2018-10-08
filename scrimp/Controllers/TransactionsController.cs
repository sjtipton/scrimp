using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using scrimp.Dtos;
using scrimp.Entities;
using scrimp.Helpers;
using scrimp.Services;

namespace scrimp.Controllers
{
    [ApiController]
    [Route("api")]
    public class TransactionsController : ControllerBase
    {
        private ITransactionService _transactionService;
        private IMapper _mapper;

        public TransactionsController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        // GET api/transaction_accounts/:userid/transactions
        [HttpGet]
        [Route("transaction_accounts/{userid}/transactions")]
        public IActionResult GetTransactionAccountTransactions(int userid)
        {
            var transactionAccountTransactions = _transactionService.GetTransactionAccountTransactions(userid);
            var transactionAccountTransactionDtos = _mapper.Map<TransactionDto>(transactionAccountTransactions);
            return Ok(transactionAccountTransactionDtos);
        }

        // GET api/accounts/:userid/transactions
        [HttpGet]
        [Route("accounts/{userid}/transactions")]
        public IActionResult GetAccountTransactions(int userid)
        {
            var accountTransactions = _transactionService.GetAccountTransactions(userid);
            var accountTransactionDtos = _mapper.Map<TransactionDto>(accountTransactions);
            return Ok(accountTransactionDtos);
        }

        // GET api/users/:userid/transactions
        [HttpGet]
        [Route("users/{userid}/transactions")]
        public IActionResult GetUserTransactions(int userid)
        {
            var userTransactions = _transactionService.GetUserTransactions(userid);
            var userTransactionDtos = _mapper.Map<TransactionDto>(userTransactions);
            return Ok(userTransactionDtos);
        }

        // POST api/transaction_accounts/:userid/transactions
        [HttpPost]
        [Route("transaction_accounts/{userid}/transactions")]
        public IActionResult CreateTransactionAccountTransaction(int userid, [FromBody]TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);

            try
            {
                _transactionService.CreateTransactionAccountTransaction(userid, transaction);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
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
