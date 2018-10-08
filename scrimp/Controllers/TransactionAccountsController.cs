using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using scrimp.Dtos;
using scrimp.Services;

namespace scrimp.Controllers
{
    [ApiController]
    [Route("api")]
    public class TransactionAccountsController : ControllerBase
    {
        private ITransactionAccountService _transactionAccountService;
        private IMapper _mapper;

        public TransactionAccountsController(ITransactionAccountService transactionAccountService, IMapper mapper)
        {
            _transactionAccountService = transactionAccountService;
            _mapper = mapper;
        }

        // GET api/users/:id/transaction_accounts
        [HttpGet]
        [Route("users/{id}/transaction_accounts")]
        public IActionResult GetUserTransactionAccounts(int id)
        {
            var userTransactionAccounts = _transactionAccountService.GetUserTransactionAccounts(id);
            var userTransactionAccountDtos = _mapper.Map<TransactionAccountDto>(userTransactionAccounts);
            return Ok(userTransactionAccountDtos);
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
