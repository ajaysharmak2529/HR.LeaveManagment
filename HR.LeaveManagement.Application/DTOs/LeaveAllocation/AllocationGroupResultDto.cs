namespace HR.LeaveManagement.Application.DTOs.LeaveAllocation
{
    public class AllocationGroupResultDto
    {
        public string LeaveType { get; set; } = string.Empty;
        public int Year { get; set; }
        public int EmployeeCount { get; set; }
    }
}
