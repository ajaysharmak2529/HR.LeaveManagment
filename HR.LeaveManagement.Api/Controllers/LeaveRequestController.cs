using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IList<LeaveRequestListDto>>> Get()
        {
            var LeaveRequests = await _mediator.Send(new GetLeaveRequestListRequest());
            return LeaveRequests;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDto>> Get(int id)
        {
            var LeaveRequests = await _mediator.Send(new GetLeaveRequestDetailRequest());
            return LeaveRequests;
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveRequestDto LeaveRequestDto)
        {
            var LeaveRequests = await _mediator.Send(new CreateLeaveRequestCommand() { LeaveRequestDto = LeaveRequestDto });
            return LeaveRequests;
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveRequestDto LeaveRequestDto)
        {
            await _mediator.Send(new UpdateLeaveRequestCommand() { LeaveRequestDto = LeaveRequestDto });
            return NoContent();
        }
        [HttpPut("changeapproval")]
        public async Task<ActionResult> ChangeApproval([FromBody] ChangesLeaveRequestApprovalDto LeaveAllocationDto)
        {
             await _mediator.Send(new UpdateLeaveRequestCommand{LeaveRequestApprovalDto  = LeaveAllocationDto });

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteLeaveRequestRequestCommand() { Id = id });
            return NoContent();
        }
    }
}
