using ECommerce.Core.DTO;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenServices _tokenServices;

        public AccountController(ITokenServices tokenServices)
        {
            _tokenServices = tokenServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _tokenServices.registerAsync(model);

            if (!result.IsAuthanticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _tokenServices.GetTokenAsync(model);

            if (!result.IsAuthanticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpGet("Get-Current-User")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserAsync()
        {
            var email = User?.Claims?.FirstOrDefault(e => e.Type == ClaimTypes.Email).Value;
            return Ok(await _tokenServices.GetCurrentUser(email));
        }

       

    }
}
