using Microsoft.AspNetCore.Mvc;
using PasswordManager.Models.Dtos.InfoDtos;
using PasswordManager.Models.Enums;
using PasswordManager.Repositories.Contracts;
using System.Security.Claims;

namespace PasswordManager.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class InfoController : BaseController
    {
        private IInfoRepository infoRepository { get; set; }

        public InfoController(IInfoRepository infoRepository)
        {
            this.infoRepository = infoRepository;
        }

        // Ivana
        [HttpGet("Mine")]
        public async Task<IActionResult> GetMine()
        {
            string email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var myInfos = await infoRepository.GetMyInfos(email);

            return Ok(myInfos);
        }

        // Alex
        [HttpGet("Shared")]
        public async Task<IActionResult> GetShared()
        {
            string email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var sharedInfos = await infoRepository.GetSharedInfos(email);

            return Ok(sharedInfos);
        }

        // Georgi
        [HttpGet("Sort")]
        public async Task<IActionResult> GetMineSorted([FromQuery] int type)
        {
            string email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var mySortedInfos = await infoRepository.SortMyInfos(email, type);

            return Ok(mySortedInfos);
        }

        // Ivana
        [HttpPost("Add")]
        public async Task<IActionResult> AddInfo([FromBody] InfoAddDto infoDto)
        {
            string email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            await infoRepository.AddInfo(email, infoDto);

            return Ok();
        }

        // Ivana
        [HttpPost("Share")]
        public async Task<IActionResult> ShareInfo([FromQuery] int friendId, int infoId)
        {
            string email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            await infoRepository.ShareInfo(email, friendId, infoId);

            return Ok();
        }
    }
}
