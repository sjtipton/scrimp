using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private IErrorService _errorService;
        private IMapper _mapper;

        public UsersController(IUserService userService, IErrorService errorService, IMapper mapper)
        {
            _userService = userService;
            _errorService = errorService;
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
                return NotFound(_errorService.NotFound("user", id));
            }

            if (user is User)
            {
                var userDto = _mapper.Map<UserDto>(user);
                return Ok(userDto);
            }
            return BadRequest(_errorService.BadRequest("user", id));
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
                return BadRequest(_errorService.BadRequest(ex));
            }
        }
    }
}
