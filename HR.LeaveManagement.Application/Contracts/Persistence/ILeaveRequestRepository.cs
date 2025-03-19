using HR.LeaveManagement.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<LeaveRequest> GetLeaveRequestWithDetailAsync(int id);
        Task<List<LeaveRequest>> GetAllLeaveRequestsWithDetailAsync();
        Task ChangeLeaveRequestApproval(LeaveRequest leaveRequest, bool? approvalStatus);
        Task<int> GetEmployeeTotalLeaveRequest(string userId);
        Task<int> GetEmployeePendingLeaveRequest(string userId);
        Task<int> GetEmployeeApprovedLeaveRequest(string userId);
        Task<int> GetEmployeeRejectedLeaveRequest(string userId);
        Task<int> GetTotalLeaveRequest();
        Task<int> GetPendingLeaveRequest();
        Task<int> GetApprovedLeaveRequest();
        Task<int> GetRejectedLeaveRequest();
    }
}
