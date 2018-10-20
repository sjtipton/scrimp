using scrimp.Entities;
using System;

namespace scrimp.Services
{
    public interface IUserService
    {
        User GetById(int id);
        User GetByApiId(Guid apiId);
        User Create(User user);
        void Update(User user);
    }
}
