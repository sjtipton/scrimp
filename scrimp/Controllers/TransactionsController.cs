using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using scrimp.Dtos;
using scrimp.Entities;
using scrimp.Helpers;
using scrimp.Services;

namespace scrimp.Controllers
{
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private ITransactionService _transactionService;
        private IMapper _mapper;

        public TransactionsController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        // GET transaction_accounts/:id/transactions
        [HttpGet("transaction_accounts/:id/transactions")]
        public IActionResult GetTransactionAccountTransactions(int id)
        {
            var transactionAccountTransactions = _transactionService.GetTransactionAccountTransactions(id);
            var transactionAccountTransactionDtos = _mapper.Map<TransactionDto>(transactionAccountTransactions);
            return Ok(transactionAccountTransactionDtos);
        }

        // GET accounts/:id/transactions
        [HttpGet("accounts/:id/transactions")]
        public IActionResult GetAccountTransactions(int id)
        {
            var accountTransactions = _transactionService.GetAccountTransactions(id);
            var accountTransactionDtos = _mapper.Map<TransactionDto>(accountTransactions);
            return Ok(accountTransactionDtos);
        }

        // GET users/:id/transactions
        [HttpGet("users/:id/transactions")]
        public IActionResult GetUserTransactions(int id)
        {
            var userTransactions = _transactionService.GetUserTransactions(id);
            var userTransactionDtos = _mapper.Map<TransactionDto>(userTransactions);
            return Ok(userTransactionDtos);
        }

        // POST transaction_accounts/:id/transactions
        [HttpPost("transaction_accounts/:id/transactions")]
        public IActionResult CreateTransactionAccountTransaction(int id, [FromBody]TransactionDto transactionDto)
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

        // GET transactions/:id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var transaction = _transactionService.GetById(id);
            var transactionDto = _mapper.Map<TransactionDto>(transaction);
            return Ok(transactionDto);
        }

        // PUT transactions/:id
        [HttpPut("{id}")]
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
