using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands
{
    public class DeleteLeaveRequestRequestCommand : IRequest<BaseCommandResponse>
    {
        public int Id { get; set; }
    }
}
