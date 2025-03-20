using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> Get()
        {
            try
            {
                var leaveTypes = await _mediator.Send(new GetLeaveTypeListRequest());
                return Ok(ApiResponse<IList<LeaveTypeDto>>.Success(leaveTypes, StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<IList<LeaveTypeDto>>.Fail("Something went wrong", StatusCodes.Status500InternalServerError, new string[] { ex.Message }));
            }
        }

        [HttpGet("{id}/Get")]
        public async Task<ActionResult<LeaveTypeDto>> Get(int id)
        {
            try
            {
                var leaveTypes = await _mediator.Send(new GetLeaveTypeDetailRequest() { Id = id });
                return Ok(ApiResponse<LeaveTypeDto>.Success(leaveTypes, StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<LeaveTypeDto>.Fail("Something went wrong", StatusCodes.Status500InternalServerError, new string[] { ex.Message }));
            }
        }
        [Authorize(Roles ="Admin")]
        [HttpPost("Create")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveTypeDto leaveTypeDto)
        {
            try
            {
                var result = await _mediator.Send(new CreateLeaveTypeCommand() { LeaveTypeDto = leaveTypeDto });
                if (result.Success)
                {
                    return Ok(ApiResponse<BaseCommandResponse>.Success(result, StatusCodes.Status201Created,"Created successfully."));
                }
                else
                {
                    return BadRequest(ApiResponse<BaseCommandResponse>.Fail(result.Message, StatusCodes.Status400BadRequest, result.Errors));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<BaseCommandResponse>.Fail("Something went wrong", StatusCodes.Status500InternalServerError, new string[] { ex.Message }));
            }
        }
        [Authorize(Roles ="Admin")]
        [HttpPut("Update")]
        public async Task<ActionResult> Put([FromBody] LeaveTypeDto leaveTypeDto)
        {
            try
            {
                var result = await _mediator.Send(new UpdateLeaveTypeCommand() { LeaveTypeDto = leaveTypeDto });

                if (result.Success)
                {
                    return Ok(ApiResponse<BaseCommandResponse>.Success(result, StatusCodes.Status200OK, "Updated successfully."));
                }
                else
                {
                    return BadRequest(ApiResponse<BaseCommandResponse>.Fail(result.Message, StatusCodes.Status400BadRequest, result.Errors));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail("Something went wrong", StatusCodes.Status500InternalServerError, new string[] { ex.Message }));
            }
        }
        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}/Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteLeaveTypeRequestCommand() { Id = id });
                if (result.Success)
                {
                    return Ok(ApiResponse<string>.Success(null!, StatusCodes.Status200OK, "Deleted successfully."));
                }
                else
                {
                    return BadRequest(ApiResponse<BaseCommandResponse>.Fail(result.Message, StatusCodes.Status400BadRequest, result.Errors));
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail("Something went wrong", StatusCodes.Status500InternalServerError, new string[] { ex.Message }));
            }
        }
    }
}
