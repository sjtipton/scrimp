﻿using Microsoft.AspNetCore.Mvc;
using scrimp.Dtos;
using System;

namespace scrimp.Controllers
{
    // TODO add Authorization
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        // TODO implement DI
        public UsersController() { }

        // TODO consider authenticate/authorize/register endpoints

        // GET me
        [HttpGet("~/me")]
        public IActionResult GetAuthenticatedUser()
        {
            throw new NotImplementedException();
        }

        // GET users/:id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            throw new NotImplementedException();
        }

        // PUT users/:id
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
