using HR.LeaveManagement.Application.Contracts.Persistence;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private LeaveManagementDbContext _dbContext { get; set; }
        public UnitOfWork(LeaveManagementDbContext leaveManagementDbContext)
        {
            LeaveAllocations = new LeaveAllocationRepository(leaveManagementDbContext);
            LeaveTypes = new LeaveTypeRepository(leaveManagementDbContext);
            LeaveRequests = new LeaveRequestRepository(leaveManagementDbContext);
        }
        public ILeaveAllocationRepository LeaveAllocations { get; set; }
        public ILeaveTypeRepository LeaveTypes { get; set; }
        public ILeaveRequestRepository LeaveRequests { get; set; }


        public async Task SaveChangesAsync()
        {
            try
            {
               await _dbContext.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {

            }
        }
    }
}
