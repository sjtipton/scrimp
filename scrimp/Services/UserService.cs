using scrimp.Dtos;
using scrimp.Entities;
using scrimp.Helpers;
using System;
using System.Linq;

namespace scrimp.Services
{
    public class UserService : IUserService
    {
        private DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public AuthenticatedUser GetAuthenticatedUser()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
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
