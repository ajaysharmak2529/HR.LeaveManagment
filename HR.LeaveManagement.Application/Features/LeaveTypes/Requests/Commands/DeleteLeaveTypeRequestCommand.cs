using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands
{
    public class DeleteLeaveTypeRequestCommand : IRequest<BaseCommandResponse>
    {
        public int Id { get; set; }
    }
}
