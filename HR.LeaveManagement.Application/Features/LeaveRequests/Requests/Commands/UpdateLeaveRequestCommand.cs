using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands
{
    public class UpdateLeaveRequestCommand : IRequest
    {
        public UpdateLeaveRequestDto LeaveRequestDto { get; set; } = null!;
        public ChangesLeaveRequestApprovalDto LeaveRequestApprovalDto { get; set; } = null!;
    }
}
