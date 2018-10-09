using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using scrimp.Domain;
using scrimp.Dtos;
using scrimp.Entities;
using scrimp.Helpers;
using scrimp.Services;
using System;

namespace scrimp.Controllers
{
    // TODO add Authorization
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // TODO consider authenticate/authorize/register endpoints

        // GET api/me
        [HttpGet]
        [Route("~/api/me")]
        public IActionResult GetAuthenticatedUser()
        {
            throw new NotImplementedException();
        }

        // GET api/users/:id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);

            if (user == null)
            {
                // TODO expand on this pattern and then refactor into a helper
                return NotFound(new Error {
                    Id = Guid.NewGuid(),
                    Status = HttpStatus.NotFound,
                    Code = HttpStatus.NotFound.ToString(),
                    Title = "User Not Found",
                    Detail = $"The User identified by id {id} was not found."
                });
            }

            if (user is User)
            {
                var userDto = _mapper.Map<UserDto>(user);
                return Ok(userDto);
            }
            //return BadRequest("The user is not valid.");
            return BadRequest(new Error
            {
                Id = Guid.NewGuid(),
                Status = HttpStatus.BadRequest,
                Code = HttpStatus.BadRequest.ToString(),
                Title = "Bad Request",
                Detail = $"The User is not valid."
            });
        }

        // PUT api/users/:id
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Id = id;

            try
            {
                _userService.Update(user);
                return Ok();
            }
            catch(AppException ex)
            {
                //return BadRequest(new { message = ex.Message });
                return BadRequest(new Error
                {
                    Id = Guid.NewGuid(),
                    Status = HttpStatus.BadRequest,
                    Code = HttpStatus.BadRequest.ToString(),
                    Title = "Bad Request",
                    Detail = ex.Message,
                    InnerException = ex
                });
            }
        }
    }
}
