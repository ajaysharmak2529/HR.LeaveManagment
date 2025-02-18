﻿using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands
{
    public class UpdateLeaveAllocationCommand : IRequest<BaseCommandResponse>
    {
        public UpdateLeaveAllocationDto LeaveAllocationDto { get; set; } = null!;
    }
}
