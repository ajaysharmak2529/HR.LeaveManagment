using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/employees")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> Me(string userId)
        {
            var result = await _userService.GetEmployee(userId);
            if (result.IsSuccess)
                return Ok(ApiResponse<Employee>.Success(result.Data!, StatusCodes.Status200OK, result.Message));
            else
                return BadRequest(ApiResponse<Employee>.Fail(result.Message!, StatusCodes.Status400BadRequest, result.Errors));
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetEmployees();

                if (result.IsSuccess)
                    return Ok(ApiResponse<List<Employee>>.Success(result.Data!, StatusCodes.Status200OK, result.Message));
                else
                    return BadRequest(ApiResponse<List<Employee>>.Fail(result.Message!, StatusCodes.Status400BadRequest, result.Errors));
        }
    }
}
