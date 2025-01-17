using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
          return await _dbContext.LeaveAllocations.Include(x=> x.leaveType).ToListAsync();
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetailAsync(int id)
        {
            return await _dbContext.LeaveAllocations.Include(x => x.leaveType).FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
