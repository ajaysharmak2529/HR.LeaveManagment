using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        public ILeaveAllocationRepository LeaveAllocations { get; set; }
        public ILeaveTypeRepository LeaveTypes { get; set; }
        public ILeaveRequestRepository LeaveRequests { get; set; }

        Task SaveChangesAsync();
    }
}
