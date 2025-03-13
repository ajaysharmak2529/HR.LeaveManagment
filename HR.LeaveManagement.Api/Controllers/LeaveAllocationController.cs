using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveAllocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> Get()
        {
            try
            {
                var LeaveAllocations = await _mediator.Send(new GetLeaveAllocationListRequest());
                return Ok(ApiResponse<IList<LeaveAllocationDto>>.Success(LeaveAllocations, StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<IList<LeaveAllocationDto>>.Fail("Something went wrong", StatusCodes.Status500InternalServerError, new string[] { ex.Message }));
            }
        }

        [HttpGet("{id}/Get")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var LeaveAllocations = await _mediator.Send(new GetLeaveAllocationDetailRequest() { Id = id });
                return Ok(ApiResponse<LeaveAllocationDto>.Success(LeaveAllocations, StatusCodes.Status200OK));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<LeaveAllocationDto>.Fail("Something went wrong", StatusCodes.Status500InternalServerError, new string[] { ex.Message }));
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Post([FromBody] CreateLeaveAllocationDto LeaveAllocationDto)
        {
            try
            {
                var LeaveAllocations = await _mediator.Send(new CreateLeaveAllocationCommand() { LeaveAllocationDto = LeaveAllocationDto });
                if (LeaveAllocations.Success)
                {
                    return Ok(ApiResponse<string>.Success(null!, StatusCodes.Status201Created, LeaveAllocations.Message));
                }
                else
                {
                    return BadRequest(ApiResponse<string>.Fail(LeaveAllocations.Message, StatusCodes.Status400BadRequest, LeaveAllocations.Errors));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<LeaveAllocationDto>.Fail("Something went wrong", StatusCodes.Status500InternalServerError, new string[] { ex.Message }));
            }
        }


        [HttpPut("Update")]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveAllocationDto LeaveAllocationDto)
        {
            try
            {
                var result = await _mediator.Send(new UpdateLeaveAllocationCommand() { LeaveAllocationDto = LeaveAllocationDto });
                if (result.Success)
                {
                    return Ok(ApiResponse<string>.Success(null!, StatusCodes.Status200OK, result.Message));
                }
                else
                {
                    return BadRequest(ApiResponse<string>.Fail(result.Message, StatusCodes.Status400BadRequest, result.Errors));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<LeaveAllocationDto>.Fail("Something went wrong", StatusCodes.Status500InternalServerError, new string[] { ex.Message }));
            }
        }

        [HttpDelete("{id}/Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteLeaveAllocationRequestCommand() { Id = id });
                if (result.Success)
                {
                    return Ok(ApiResponse<string>.Success(null!, StatusCodes.Status200OK, result.Message));
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
    }
}
