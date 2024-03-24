using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PasswordManager.Repositories.Contracts;

namespace PasswordManager.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class PasswordController : BaseController
    {
        private readonly IPasswordRepository passwordRepository;

        public PasswordController(IPasswordRepository passwordRepository)
        {
            this.passwordRepository = passwordRepository;
        }

        // Ivana
        [HttpGet("CheckPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckPasswordStength([FromQuery] string password)
        {
            var response = await passwordRepository.CheckPasswordStrength(password);

            return Ok(response);
        }
    }
}
