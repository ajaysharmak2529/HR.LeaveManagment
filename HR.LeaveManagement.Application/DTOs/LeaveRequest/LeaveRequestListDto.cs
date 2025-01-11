using HR.LeaveManagement.Application.DTOs.Common;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using System;

namespace HR.LeaveManagement.Application.DTOs.LeaveRequest
{
    public class LeaveRequestListDto : BaseDto
    {
        public LeaveTypeDto LeaveType { get; set; } = null!;
        public DateTime LeaveRequested { get; set; }
        public bool? Approved { get; set; }
    }
}
