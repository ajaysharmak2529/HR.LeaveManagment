using HR.LeaveManagement.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task<LeaveAllocation> GetLeaveAllocationWithDetailAsync(int id);
        Task<List<LeaveAllocation>> GetAllLeaveAllocationsWithDetailAsync();
        Task<List<LeaveAllocation>> GetAllEmployeeLeaveAllocationsWithDetailAsync(string userId);
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync(string userId);
        Task<bool> AllocationExistsAsync(string userId, int leaveTypeId, int period);
        Task AddAllocationsAsync(List<LeaveAllocation> allocations);
        Task<LeaveAllocation> GetUserAllocationsAsync(string userId, int leaveTypeId);
    }
}
