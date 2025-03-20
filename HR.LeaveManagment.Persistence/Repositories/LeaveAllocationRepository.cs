using HR.LeaveManagement.Application.Contracts.Persistence;
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

        public async Task<List<LeaveAllocation>> GetAllLeaveAllocationsWithDetailAsync()
        {
          return await _dbContext.LeaveAllocations.Include(x=> x.LeaveType).ToListAsync();
        }
        public async Task<List<LeaveAllocation>> GetAllAdminLeaveAllocationsWithDetailAsync()
        {
            throw new NotImplementedException();
          //return await _dbContext.LeaveAllocations.Include(x=> x.LeaveType).GroupBy(x=>x.Period);
        }
        public async Task<List<LeaveAllocation>> GetAllEmployeeLeaveAllocationsWithDetailAsync(string userId)
        {
          return await _dbContext.LeaveAllocations.Where(x=>x.EmployeeId == userId && x.Period == DateTime.Now.Year).Include(x=> x.LeaveType).ToListAsync();
        }

        public async Task<LeaveAllocation?> GetLeaveAllocationWithDetailAsync(int id)
        {
            return await _dbContext.LeaveAllocations.Include(x => x.LeaveType).FirstOrDefaultAsync(x=>x.Id == id);
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
        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync(string userId)
        {
            var leaveAllocations = await _dbContext.LeaveAllocations.Where(q => q.EmployeeId == userId)
               .Include(q => q.LeaveType)
               .ToListAsync();
            return leaveAllocations;
        }
        public async Task<LeaveAllocation> GetUserAllocationsAsync(string userId, int leaveTypeId)
        {
            return await _dbContext.LeaveAllocations.FirstOrDefaultAsync(q => q.EmployeeId == userId
                                        && q.LeaveTypeId == leaveTypeId);
        }
    }
}
