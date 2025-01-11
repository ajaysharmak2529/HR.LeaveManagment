using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Domain
{
    public class LeaveAllocation : BaseDomainEntity
    {
        public int NumberOfDays { get; set; }
        public LeaveType leaveType { get; set; } = null!;
        public int leaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
