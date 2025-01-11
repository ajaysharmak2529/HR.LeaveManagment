using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands
{
    public class DeleteLeaveAllocationRequestCommand : IRequest
    {
        public int Id { get; set; }
    }
}
