using scrimp.Dtos;
using scrimp.Entities;

namespace scrimp.Services
{
    public interface IUserService
    {
        AuthenticatedUser GetAuthenticatedUser();
        User GetById(int id);
        void Update(User user);
    }
}
