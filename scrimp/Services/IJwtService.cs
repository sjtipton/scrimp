﻿using System.Security.Claims;
using System.Threading.Tasks;

namespace scrimp.Services
{
    public interface IJwtService
    {
        Task<string> GenerateEncodedToken(string identifier, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string identifier, string id);
        ClaimsIdentity GenerateClaimsIdentity(string identifier, int id);
    }
}
