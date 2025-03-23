using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using HR.LeaveManagement.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> Get(int? page = 1, int? pageSize = 10)
        {
            try
            {
                var LeaveRequests = await _mediator.Send(new GetLeaveRequestListRequest() { Page = page, PageSize = pageSize });
                return Ok(ApiResponse<PageList<LeaveRequestListDto>>.Success(LeaveRequests, StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail("Something went wrong", StatusCodes.Status500InternalServerError, new string[] { ex.Message }));
            }
        }

        [HttpGet("{id}/Get")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var LeaveRequests = await _mediator.Send(new GetLeaveRequestDetailRequest() { Id = id });
                return Ok(ApiResponse<LeaveRequestDto>.Success(LeaveRequests, StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail("Something went wrong", StatusCodes.Status500InternalServerError, new string[] { ex.Message }));
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Post([FromBody] CreateLeaveRequestDto LeaveRequestDto)
        {
            try
            {
                var result = await _mediator.Send(new CreateLeaveRequestCommand() { LeaveRequestDto = LeaveRequestDto });
                if (result.Success)
                {
                    return Ok(ApiResponse<string>.Success(result.Message, StatusCodes.Status201Created));
                }
                else
                {
                    return BadRequest(ApiResponse<string>.Fail(result.Message, StatusCodes.Status400BadRequest, result.Errors));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail("Something went wrong", StatusCodes.Status500InternalServerError, new string[] { ex.Message }));
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveRequestDto LeaveRequestDto)
        {
            try
            {
                var result = await _mediator.Send(new UpdateLeaveRequestCommand() { LeaveRequestDto = LeaveRequestDto });
                if (result.Success)
                {
                    return Ok(ApiResponse<string>.Success(null!, StatusCodes.Status200OK, "Updated successfully"));
                }
                else
                {
                    return BadRequest(ApiResponse<string>.Fail(result.Message, StatusCodes.Status400BadRequest, result.Errors));
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail("Something went wrong", StatusCodes.Status500InternalServerError, new string[] { ex.Message }));
            }
        }
        [HttpPut("ChangeApproval")]
        public async Task<ActionResult> ChangeApproval([FromBody] ChangesLeaveRequestApprovalDto LeaveAllocationDto)
        {
            try
            {
                var result = await _mediator.Send(new UpdateLeaveRequestCommand { LeaveRequestApprovalDto = LeaveAllocationDto });
                if (result.Success)
                {
                    return Ok(ApiResponse<string>.Success(null!, StatusCodes.Status200OK, "Approval changed successfully"));
                }
                else
                {
                    return BadRequest(ApiResponse<string>.Fail(result.Message, StatusCodes.Status400BadRequest, result.Errors));
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail("Something went wrong", StatusCodes.Status500InternalServerError, new string[] { ex.Message }));
            }
        }
        [HttpDelete("{id}/Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteLeaveRequestRequestCommand() { Id = id });

                if (result.Success)
                {
                    return Ok(ApiResponse<string>.Success(null!, StatusCodes.Status200OK, "Deleted successfully"));
                }
                else
                {
                    return BadRequest(ApiResponse<string>.Fail(result.Message, StatusCodes.Status400BadRequest, result.Errors));
                }



            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail("Something went wrong", StatusCodes.Status500InternalServerError, new string[] { ex.Message }));
            }
        }

        [HttpGet("Employee")]
        public async Task<IActionResult> GetEmployeeLeaveRequest(int? page = 1, int? pageSize = 10)
        {
            try
            {
                var list = await _mediator.Send(new GetEmployeeLeaveRequestListRequest() { Page = page, PageSize = pageSize });
                return Ok(ApiResponse<List<LeaveRequestListDto>>.Success(list, StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message, StatusCodes.Status400BadRequest,new string[] {ex.Message}));
            }
        }
    }
}
