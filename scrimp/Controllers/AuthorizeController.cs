using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace scrimp.Controllers
{
    [Authorize(Policy = "ApiUser")]
    public abstract class AuthorizeController : ControllerBase
    {
    }
}
