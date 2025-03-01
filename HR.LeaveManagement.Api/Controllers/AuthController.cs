using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest model)
        {
            var result = await _authService.Register(model);            
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest model)
        {
            var result = await _authService.Login(model);            
            return Ok(result);
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromQuery] string refreshToken)
        {
            var result = await _authService.RefreshUserToken(refreshToken);
            if (!result.Success)
            {
                return BadRequest(new {result.Success, result.Message });
            }
            return Ok(result);
        }
        
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogOut();            
            return Ok();
        }
    }
}
