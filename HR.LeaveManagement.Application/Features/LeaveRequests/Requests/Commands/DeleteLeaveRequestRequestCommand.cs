using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands
{
    public class DeleteLeaveRequestRequestCommand : IRequest
    {
        public int Id { get; set; }
    }
}
