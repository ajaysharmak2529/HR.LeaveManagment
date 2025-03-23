using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly LeaveManagementDbContext _dbContext;

        public LeaveAllocationRepository(LeaveManagementDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PageList<LeaveAllocation>> GetAllLeaveAllocationsWithDetailAsync(int? page, int? pageSize)
        {
            var query = _dbContext.LeaveAllocations.Include(x => x.LeaveType);
            return await PageList<LeaveAllocation>.CreateAsync(query, page!.Value, pageSize!.Value);
        }
        public async Task<PageList<AllocationGroupResultDto>> GetAllAdminLeaveAllocationsWithDetailAsync(int? page, int? pageSize)
        {
            var result = _dbContext.LeaveAllocations.Include(x => x.LeaveType).GroupBy(x => new { x.Period, x.LeaveTypeId, x.LeaveType }).Select(g =>
            new AllocationGroupResultDto
            {
                LeaveType = g.Key.LeaveType.Name,
                Year = g.Key.Period,
                EmployeeCount = g.Count()
            });

            return await PageList<AllocationGroupResultDto>.CreateAsync(result, page!.Value, pageSize!.Value);
        }
        public async Task<PageList<LeaveAllocation>> GetAllEmployeeLeaveAllocationsWithDetailAsync(string userId, int? page, int? pageSize)
        {
            var query = _dbContext.LeaveAllocations.Where(x => x.EmployeeId == userId && x.Period == DateTime.Now.Year).Include(x => x.LeaveType);
            return await PageList<LeaveAllocation>.CreateAsync(query, page!.Value, pageSize!.Value);
        }

        public async Task<LeaveAllocation?> GetLeaveAllocationWithDetailAsync(int id)
        {
            return await _dbContext.LeaveAllocations.Include(x => x.LeaveType).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task AddAllocationsAsync(List<LeaveAllocation> allocations)
        {
            await _dbContext.AddRangeAsync(allocations);
        }
        public async Task<bool> AllocationExistsAsync(string userId, int leaveTypeId, int period)
        {
            return await _dbContext.LeaveAllocations.AnyAsync(q => q.EmployeeId == userId
                                        && q.LeaveTypeId == leaveTypeId
                                        && q.Period == period);
        }
        public async Task<PageList<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync(string userId, int? page, int? pageSize)
        {
            var leaveAllocations = _dbContext.LeaveAllocations.Where(q => q.EmployeeId == userId).Include(q => q.LeaveType);
            return await PageList<LeaveAllocation>.CreateAsync(leaveAllocations, page!.Value, pageSize!.Value);
        }
        public async Task<LeaveAllocation?> GetUserAllocationsAsync(string userId, int leaveTypeId)
        {
            return await _dbContext.LeaveAllocations.FirstOrDefaultAsync( q => q.EmployeeId == userId && q.LeaveTypeId == leaveTypeId);
        }
    }
}
