using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands
{
    public class DeleteLeaveAllocationRequestCommand : IRequest<BaseCommandResponse>
    {
        public int Id { get; set; }
    }
}
