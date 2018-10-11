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
        private IErrorService _errorService;
        private IMapper _mapper;

        public TransactionAccountsController(IUserService userService, ITransactionAccountService transactionAccountService, IErrorService errorService, IMapper mapper)
        {
            _userService = userService;
            _transactionAccountService = transactionAccountService;
            _errorService = errorService;
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
                return NotFound(_errorService.NotFound("user", id, HttpContext.Request));
            }

            if (user is User)
            {
                var userTransactionAccounts = _transactionAccountService.GetUserTransactionAccounts(id);
                var userTransactionAccountDtos = _mapper.Map<IEnumerable<TransactionAccountDto>>(userTransactionAccounts);
                return Ok(userTransactionAccountDtos);
            }
            return BadRequest(_errorService.BadRequest("user", id, HttpContext.Request));
        }

        // GET api/transaction_accounts/:id
        [HttpGet("transaction_accounts/{id}")]
        public IActionResult GetById(int id)
        {
            var transactionAccount = _transactionAccountService.GetById(id);

            if (transactionAccount == null)
            {
                return NotFound(_errorService.NotFound("transaction account", id, HttpContext.Request));
            }

            if (transactionAccount is TransactionAccount)
            {
                var transactionAccountDto = _mapper.Map<TransactionAccountDto>(transactionAccount);
                return Ok(transactionAccountDto);
            }
            return BadRequest(_errorService.BadRequest("transaction account", id, HttpContext.Request));
        }
    }
}
