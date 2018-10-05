using scrimp.Entities;

namespace scrimp.Dtos
{
    public class AuthenticatedUser : User
    {
        public string Token { get; set; }
    }
}
