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
    [Route("api/users/{id}/accounts")]
    public class AccountsController : ControllerBase
    {
        private IAccountService _accountService;
        private IMapper _mapper;

        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        // GET api/users/:id/accounts
        [HttpGet]
        public IActionResult GetUserAccounts(int id)
        {
            var userAccounts = _accountService.GetUserAccounts(id);
            var userAccountDtos = _mapper.Map<IEnumerable<AccountDto>>(userAccounts);
            return Ok(userAccountDtos);
        }

        // POST api/users/:id/accounts
        [HttpPost]
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

        // GET api/accounts/:id
        [HttpGet("~/api/accounts/{id}")]
        public IActionResult GetById(int id)
        {
            var account = _accountService.GetById(id);
            var accountDto = _mapper.Map<AccountDto>(account);
            return Ok(accountDto);
        }

        // PUT api/accounts/:id
        [HttpPut("~/api/accounts/{id}")]
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

        // DELETE api/accounts/:id
        [HttpDelete("~/api/accounts/{id}")]
        public IActionResult Delete(int id)
        {
            _accountService.Delete(id);
            return Ok();
        }
    }
}
