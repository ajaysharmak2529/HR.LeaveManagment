using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(LeaveManagementDbContext leaveManagementDbContext)
        {
            LeaveAllocations = new LeaveAllocationRepository(leaveManagementDbContext);
            LeaveTypes = new LeaveTypeRepository(leaveManagementDbContext);
            LeaveRequests = new LeaveRequestRepository(leaveManagementDbContext);
        }
        public ILeaveAllocationRepository LeaveAllocations { get; set; }
        public ILeaveTypeRepository LeaveTypes { get; set; }
        public ILeaveRequestRepository LeaveRequests { get; set; }
    }
}
