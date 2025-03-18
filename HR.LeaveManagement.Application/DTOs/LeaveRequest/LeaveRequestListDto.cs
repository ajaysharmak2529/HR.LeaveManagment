using HR.LeaveManagement.Application.DTOs.Common;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using System;

namespace HR.LeaveManagement.Application.DTOs.LeaveRequest
{
    public class LeaveRequestListDto : BaseDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RequestComments { get; set; } = string.Empty;
        public LeaveTypeDto LeaveType { get; set; } = null!;
        public DateTime DateRequested { get; set; }
        public bool? Approved { get; set; }
        public bool Cancelled { get; set; }
    }
}
