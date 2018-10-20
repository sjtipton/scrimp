using scrimp.Entities;
using scrimp.Helpers.Jwt;
using System;
using System.Threading.Tasks;

namespace scrimp.Services
{
    public interface IUserService
    {
        Task<JwtResponse> AuthenticateApiUser(Guid ApiId, string AuthToken);
        User GetById(int id);
        User GetByApiId(Guid apiId);
        User Create(User user);
        void Update(User user);
    }
}
