using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
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

        [HttpGet]
        public async Task<ActionResult<IList<LeaveAllocationDto>>> Get()
        {
            var LeaveAllocations = await _mediator.Send(new GetLeaveAllocationListRequest());
            return LeaveAllocations;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllocationDto>> Get(int id)
        {
            var LeaveAllocations = await _mediator.Send(new GetLeaveAllocationDetailRequest());
            return LeaveAllocations;
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveAllocationDto LeaveAllocationDto)
        {
            var LeaveAllocations = await _mediator.Send(new CreateLeaveAllocationCommand() { LeaveAllocationDto = LeaveAllocationDto });
            return LeaveAllocations;
        }
        

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveAllocationDto LeaveAllocationDto)
        {
            await _mediator.Send(new UpdateLeaveAllocationCommand() { LeaveAllocationDto = LeaveAllocationDto });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteLeaveAllocationRequestCommand() { Id = id });
            return NoContent();
        }
    }
}
