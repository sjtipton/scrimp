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
        private IUserService _userService;
        private IAccountService _accountService;
        private IErrorService _errorService;
        private IMapper _mapper;

        public AccountsController(IUserService userService, IAccountService accountService, IErrorService errorService, IMapper mapper)
        {
            _userService = userService;
            _accountService = accountService;
            _errorService = errorService;
            _mapper = mapper;
        }

        // GET api/users/:id/accounts
        [HttpGet]
        public IActionResult GetUserAccounts(int id)
        {
            var user = _userService.GetById(id);

            if (user == null)
            {
                return NotFound(_errorService.NotFound("user", id, HttpContext.Request));
            }

            if (user is User)
            {
                var userAccounts = _accountService.GetUserAccounts(id);
                var userAccountDtos = _mapper.Map<IEnumerable<AccountDto>>(userAccounts);
                return Ok(userAccountDtos);
            }
            return BadRequest(_errorService.BadRequest("user", id, HttpContext.Request));
        }

        // POST api/users/:id/accounts
        [HttpPost]
        public IActionResult CreateUserAccount(int id, [FromBody]AccountDto accountDto)
        {
            var user = _userService.GetById(id);

            if (user == null)
            {
                return NotFound(_errorService.NotFound("user", id, HttpContext.Request));
            }

            if (user is User)
            {
                var account = _mapper.Map<Account>(accountDto);

                try
                {
                    _accountService.CreateUserAccount(id, account);
                    return Ok();
                }
                catch (AppException ex)
                {
                    return BadRequest(_errorService.BadRequest(ex, HttpContext.Request));
                }
            }
            return BadRequest(_errorService.BadRequest("user", id, HttpContext.Request));
        }

        // GET api/accounts/:id
        [HttpGet("~/api/accounts/{id}")]
        public IActionResult GetById(int id)
        {
            var account = _accountService.GetById(id);

            if (account == null)
            {
                return NotFound(_errorService.NotFound("account", id, HttpContext.Request));
            }

            if (account is Account)
            {
                var accountDto = _mapper.Map<AccountDto>(account);
                return Ok(accountDto);
            }
            return BadRequest(_errorService.BadRequest("account", id, HttpContext.Request));
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
                return BadRequest(_errorService.BadRequest(ex, HttpContext.Request));
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
