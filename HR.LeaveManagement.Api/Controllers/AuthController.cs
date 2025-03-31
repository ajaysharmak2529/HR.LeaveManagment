using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Application.Models.Identity;
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
            if (result.IsSuccess)
                return Ok(ApiResponse<AuthResponse>.Success(result.Data!, StatusCodes.Status200OK, result.Message));
            else
                return BadRequest(ApiResponse<AuthResponse>.Fail(result.Message!, StatusCodes.Status400BadRequest, result.Errors));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest model)
        {
            var result = await _authService.Login(model);

            if (result.IsSuccess)
                return Ok(ApiResponse<AuthResponse>.Success(result.Data!, StatusCodes.Status200OK, result.Message));
            else
                return BadRequest(ApiResponse<AuthResponse>.Fail(result.Message!, StatusCodes.Status400BadRequest, result.Errors));
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromQuery] string refreshToken)
        {
            var result = await _authService.RefreshUserToken(refreshToken);

            if (result.IsSuccess)
                return Ok(ApiResponse<AuthResponse>.Success(result.Data!, StatusCodes.Status200OK, result.Message));
            else
                return BadRequest(ApiResponse<AuthResponse>.Fail(result.Message!, StatusCodes.Status400BadRequest, result.Errors));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogOut();

            return Ok(ApiResponse<string>.Success("",StatusCodes.Status200OK,"Logout successfully."));
        }
    }
}
