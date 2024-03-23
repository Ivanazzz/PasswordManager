using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PasswordManager.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
    }
}
