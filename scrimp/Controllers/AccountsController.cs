using Microsoft.AspNetCore.Mvc;
using scrimp.Dtos;
using System;

namespace scrimp.Controllers
{
    [ApiController]
    public class AccountsController : ControllerBase
    {
        // TODO implement DI
        public AccountsController() { }

        // GET users/:id/accounts
        [HttpGet("users/:id/accounts")]
        public IActionResult GetUserAccounts(int id)
        {
            throw new NotImplementedException();
        }

        // POST users/:id/accounts
        [HttpPost("users/:id/accounts")]
        public IActionResult CreateUserAccount(int id, [FromBody]AccountDto accountDto)
        {
            throw new NotImplementedException();
        }

        // GET accounts/:id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            throw new NotImplementedException();
        }

        // PUT accounts/:id
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]AccountDto accountDto)
        {
            throw new NotImplementedException();
        }

        // DELETE accounts/:id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
