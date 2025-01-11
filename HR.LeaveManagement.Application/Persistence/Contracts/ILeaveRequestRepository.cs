﻿using HR.LeaveManagement.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Persistence.Contracts
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<LeaveRequest> GetLeaveRequestWithDetailAsync(int id);
        Task<List<LeaveRequest>> GetAllLeaveRequestsWithDetailAsync();
        Task ChangeLeaveRequestApproval(LeaveRequest leaveRequest, bool? approvalStatus);
    }
}
