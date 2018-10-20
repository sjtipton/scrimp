using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using scrimp.Dtos;
using scrimp.Entities;
using scrimp.Helpers;
using scrimp.Helpers.Jwt;
using scrimp.Services;
using System;
using System.Threading.Tasks;

namespace scrimp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private GreenlitRestApiClient _greenlitApiClient;
        private IJwtService _jwtService;
        private JwtIssuerOptions _jwtOptions;
        private IErrorService _errorService;
        private IMapper _mapper;

        public UsersController(IUserService userService,
                               GreenlitRestApiClient greenlitApiClient,
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
            // we will already have a valid access token from Greenlit, as far as JWT is concerned
            // we just need to find out if we have a corresponding User in Scrimp to map to --
            // if not, we create that Scrimp User, and set the Greenlit API ID to the User
            // to persist the mapping (i.e. so the other User details are de-coupled)
            try
            {
                var user = _userService.GetByApiId(authDto.ApiId);

                if (user == null)
                {
                    var greenlitUser = await _greenlitApiClient.GetGreenlitRestApiUser(authDto.ApiId);

                    var appUser = new User
                    {
                        FirstName = greenlitUser.FirstName,
                        LastName = greenlitUser.LastName,
                        EmailAddress = greenlitUser.EmailAddress,
                        GreenlitApiId = greenlitUser.Id
                    };

                    var result = _userService.Create(appUser);
                }

                var localUser = _userService.GetByApiId(authDto.ApiId);

                if (localUser == null)
                {
                    return BadRequest(_errorService.BadRequest(new AppException("Failed to create local user account"), HttpContext.Request));
                }

                var jwt = await Tokens.GenerateJwt(_jwtService.GenerateClaimsIdentity(localUser.EmailAddress, localUser.Id),
                    _jwtService, localUser.EmailAddress, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwt);
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
    }
}
