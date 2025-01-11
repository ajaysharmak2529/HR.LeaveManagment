using HR.LeaveManagement.Application.DTOs.Common;

namespace HR.LeaveManagement.Application.DTOs.LeaveRequest
{
    public class ChangesLeaveRequestApprovalDto : BaseDto
    {
        public bool? Approved { get; set; }
    }
}
