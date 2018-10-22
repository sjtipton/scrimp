using Microsoft.Extensions.Options;
using scrimp.Entities;
using scrimp.Helpers;
using scrimp.Helpers.Jwt;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace scrimp.Services
{
    public class UserService : IUserService
    {
        private DataContext _context;
        private IRestApiClient<GreenlitUser> _greenlitApiClient;
        private IJwtService _jwtService;
        private JwtIssuerOptions _jwtOptions;

        public UserService(DataContext context,
                           IRestApiClient<GreenlitUser> greenlitApiClient,
                           IJwtService jwtService,
                           IOptions<JwtIssuerOptions> jwtOptions)
        {
            _context = context;
            _greenlitApiClient = greenlitApiClient;
            _jwtService = jwtService;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<JwtResponse> AuthenticateApiUser(Guid ApiId, string AuthToken)
        {
            // we will already have a valid access token from Greenlit, as far as JWT is concerned
            // we just need to find out if we have a corresponding User in Scrimp to map to --
            // if not, we create that Scrimp User, and set the Greenlit API ID to the User
            // to persist the mapping (i.e. so the other User details are de-coupled)
            var user = GetByApiId(ApiId);

            if (user == null)
            {
                var greenlitUser = await _greenlitApiClient.GetRestApiEntity(ApiId, AuthToken);

                var appUser = new User
                {
                    FirstName = greenlitUser.FirstName,
                    LastName = greenlitUser.LastName,
                    EmailAddress = greenlitUser.EmailAddress,
                    GreenlitApiId = greenlitUser.Id
                };

                var result = Create(appUser);
            }

            var localUser = GetByApiId(ApiId);

            if (localUser == null)
                throw new AppException("Failed to create local user account");

            var jwt = await Tokens.GenerateJwt(_jwtService.GenerateClaimsIdentity(localUser.EmailAddress, localUser.Id),
                _jwtService, localUser.EmailAddress, _jwtOptions);

            return jwt;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User GetByApiId(Guid apiId)
        {
            return _context.Users.Where(x => x.GreenlitApiId == apiId).SingleOrDefault();
        }

        public User Create(User user)
        {
            if (_context.Users.Any(x => x.EmailAddress == user.EmailAddress))
                throw new AppException($"Email address {user.EmailAddress} is already taken");

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(User userParam)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            if (userParam.EmailAddress != user.EmailAddress)
            {
                if (_context.Users.Any(x => x.EmailAddress == userParam.EmailAddress))
                    throw new AppException($"Email address {user.EmailAddress} is already taken");
            }

            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.EmailAddress = userParam.EmailAddress;
            user.Timezone = userParam.Timezone;
            user.WeekStartDay = userParam.WeekStartDay;
            user.CurrencyCode = userParam.CurrencyCode;

            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
