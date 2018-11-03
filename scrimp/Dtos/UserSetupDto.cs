using scrimp.Entities;
using scrimp.Helpers.Jwt;

namespace scrimp.Dtos
{
    public class UserSetupDto : UserDto
    {
        public string AuthToken { get; set; }
    }

    public class UserSetupResponse
    {
        public User User { get; set; }
        public JwtResponse Jwt { get; set; }
    }
}
