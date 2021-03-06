﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using scrimp.Dtos;
using scrimp.Entities;
using scrimp.Helpers;
using scrimp.Helpers.Jwt;
using scrimp.Services;
using System.Threading.Tasks;

namespace scrimp.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : AuthorizeController
    {
        private IUserService _userService;
        private IRestApiClient<GreenlitUser> _greenlitApiClient;
        private IJwtService _jwtService;
        private JwtIssuerOptions _jwtOptions;
        private IErrorService _errorService;
        private IMapper _mapper;

        public UsersController(IUserService userService,
                               IRestApiClient<GreenlitUser> greenlitApiClient,
                               IJwtService jwtService,
                               IOptions<JwtIssuerOptions> jwtOptions,
                               IErrorService errorService,
                               IMapper mapper)
        {
            _userService = userService;
            _greenlitApiClient = greenlitApiClient;
            _jwtService = jwtService;
            _jwtOptions = jwtOptions.Value;
            _errorService = errorService;
            _mapper = mapper;
        }

        // POST api/users/authenticate
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]GreenlitAuthDto authDto)
        {
            try
            {
                JwtResponse jwt = await _userService.AuthenticateApiUser(authDto.ApiId, authDto.AuthToken);
                return Ok(jwt);
            }
            catch (AppException ex) {
                return BadRequest(_errorService.BadRequest(ex, HttpContext.Request));
            }
        }

        // POST api/users/setup
        [AllowAnonymous]
        [HttpPost("setup")]
        public async Task<IActionResult> Setup([FromBody]UserSetupDto userSetupDto)
        {
            var authToken = userSetupDto.AuthToken;
            var user = _mapper.Map<User>(userSetupDto);

            try
            {
                var userResponse = _userService.Create(user);
                JwtResponse jwt = await _userService.AuthenticateApiUser(userResponse);

                return Ok(new UserSetupResponse { User = userResponse, Jwt = jwt });
            }
            catch (AppException ex)
            {
                return BadRequest(_errorService.BadRequest(ex, HttpContext.Request));
            }
        }

        // GET api/users/:id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);

            if (user == null)
            {
                return NotFound(_errorService.NotFound("user", id, HttpContext.Request));
            }

            if (user is User)
            {
                var userDto = _mapper.Map<UserDto>(user);
                return Ok(userDto);
            }
            return BadRequest(_errorService.BadRequest("user", id, HttpContext.Request));
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
                return BadRequest(_errorService.BadRequest(ex, HttpContext.Request));
            }
        }

        // PATCH api/users/:id
        [HttpPatch("{id}")]
        public IActionResult PatchUser(int id, [FromBody]JsonPatchDocument<User> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest(_errorService.BadRequest("user", id, HttpContext.Request));
            }

            try
            {
                var patchedUser = _userService.PatchUser(id, patchDocument);
                return Ok(patchedUser);
            }
            catch(AppException ex)
            {
                return BadRequest(_errorService.BadRequest(ex, HttpContext.Request));
            }
        }
    }
}
