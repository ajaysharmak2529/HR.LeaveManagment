using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Application.DTOs.LeaveType
{
    public class LeaveTypeDto : BaseDomainEntity, ILeaveTypeDto
    {
        public string Name { get; set; } = string.Empty;
        public int DefaultDays { get; set; }
    }
}
