using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task<LeaveAllocation> GetLeaveAllocationWithDetailAsync(int id);
        Task<PageList<LeaveAllocation>> GetAllLeaveAllocationsWithDetailAsync(int? page, int? pageSize);
        Task<PageList<AllocationGroupResultDto>> GetAllAdminLeaveAllocationsWithDetailAsync(int? page, int? pageSize);
        Task<PageList<LeaveAllocation>> GetAllEmployeeLeaveAllocationsWithDetailAsync(string userId, int? page, int? pageSize);
        Task<PageList<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync(string userId, int? page, int? pageSize);
        Task<bool> AllocationExistsAsync(string userId, int leaveTypeId, int period);
        Task AddAllocationsAsync(List<LeaveAllocation> allocations);
        Task<LeaveAllocation> GetUserAllocationsAsync(string userId, int leaveTypeId);
    }
}
