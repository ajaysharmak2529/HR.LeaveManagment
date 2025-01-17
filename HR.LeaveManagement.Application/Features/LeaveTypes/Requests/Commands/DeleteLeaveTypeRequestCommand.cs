using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands
{
    public class DeleteLeaveTypeRequestCommand : IRequest
    {
        public int Id { get; set; }
    }
}
