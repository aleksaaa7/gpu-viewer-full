using Gpuviewer.Models;
using Gpuviewer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gpuviewer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _jwtService.AuthenticateAsync(loginDto);
            if (result == null)
            {
                return Unauthorized(new { message = "wrong username or password" });
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _jwtService.RegisterAsync(registerDto);

            if (result == null || !result.success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}