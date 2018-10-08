using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using scrimp.Dtos;
using scrimp.Entities;
using scrimp.Helpers;
using scrimp.Services;

namespace scrimp.Controllers
{
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IAccountService _accountService;
        private IMapper _mapper;

        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        // GET users/:id/accounts
        [HttpGet("users/:id/accounts")]
        public IActionResult GetUserAccounts(int id)
        {
            var userAccounts = _accountService.GetUserAccounts(id);
            var userAccountDtos = _mapper.Map<AccountDto>(userAccounts);
            return Ok(userAccountDtos);
        }

        // POST users/:id/accounts
        [HttpPost("users/:id/accounts")]
        public IActionResult CreateUserAccount(int id, [FromBody]AccountDto accountDto)
        {
            var account = _mapper.Map<Account>(accountDto);

            try
            {
                _accountService.CreateUserAccount(id, account);
                return Ok();
            }
            catch(AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET accounts/:id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var account = _accountService.GetById(id);
            var accountDto = _mapper.Map<AccountDto>(account);
            return Ok(accountDto);
        }

        // PUT accounts/:id
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]AccountDto accountDto)
        {
            var account = _mapper.Map<Account>(accountDto);
            account.Id = id;

            try
            {
                _accountService.Update(account);
                return Ok();
            }
            catch(AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE accounts/:id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _accountService.Delete(id);
            return Ok();
        }
    }
}
