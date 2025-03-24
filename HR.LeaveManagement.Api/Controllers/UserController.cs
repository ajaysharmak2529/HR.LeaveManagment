using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using HR.LeaveManagement.Application.Features.Employees.Requests.Queries;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/employees")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController( IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("{userId}/Get")]
        public async Task<IActionResult> Me(string userId)
        {
            var result = await _mediator.Send(new GetEmployeeRequest() { UserId = userId});

            if (result.IsSuccess)
                return Ok(ApiResponse<Employee>.Success(result.Data!, StatusCodes.Status200OK, result.Message));
            else
                return BadRequest(ApiResponse<string>.Fail(result.Message!, StatusCodes.Status400BadRequest, result.Errors));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetUsers(int? page = 1, int pageSize = 10)
        {
            var result = await _mediator.Send(new GetEmployeesRequest() { Page = page, PageSize = pageSize });

            if (result.IsSuccess)
                    return Ok(ApiResponse<PageList<Employee>>.Success(result.Data!, StatusCodes.Status200OK, result.Message));
                else
                    return BadRequest(ApiResponse<string>.Fail(result.Message!, StatusCodes.Status400BadRequest, result.Errors));
        }
    }
}
