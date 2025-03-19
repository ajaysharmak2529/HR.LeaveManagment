using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly LeaveManagementDbContext _dbContext;

        public LeaveRequestRepository(LeaveManagementDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ChangeLeaveRequestApproval(LeaveRequest leaveRequest, bool? approvalStatus)
        {
            leaveRequest.Approved = approvalStatus;
            _dbContext.Update(leaveRequest);
            await _dbContext.SaveChangesAsync();
        }

        public Task<List<LeaveRequest>> GetAllLeaveRequestsWithDetailAsync()
        {
            var leaveRequests = _dbContext.LeaveRequests
                .Include(x=>x.LeaveType).ToListAsync();
            return leaveRequests;
        }
        public Task<List<LeaveRequest>> GetAllEmployeeLeaveRequestsWithDetailAsync(string userId)
        {
            var leaveRequests = _dbContext.LeaveRequests.Where(x=>x.EmployeeId == userId)
                .Include(x=>x.LeaveType).ToListAsync();
            return leaveRequests;
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetailAsync(int id)
        {
            var leaveRequest = await _dbContext.LeaveRequests
                .Include(x => x.LeaveType)
                .FirstOrDefaultAsync(x => x.Id == id);

            return leaveRequest;
        }
        public async Task<int> GetEmployeeTotalLeaveRequest(string userId)
        {
           return await _dbContext.LeaveRequests.Where(x=>x.EmployeeId == userId).CountAsync();                
        }
        public async Task<int> GetEmployeePendingLeaveRequest(string userId)
        {
           return await _dbContext.LeaveRequests.Where(x=>x.EmployeeId == userId && x.Approved == false).CountAsync();                
        }
        public async Task<int> GetEmployeeApprovedLeaveRequest(string userId)
        {
           return await _dbContext.LeaveRequests.Where(x=>x.EmployeeId == userId && x.Approved == true).CountAsync();                
        }
        public async Task<int> GetEmployeeRejectedLeaveRequest(string userId)
        {
           return await _dbContext.LeaveRequests.Where(x=>x.EmployeeId == userId && x.Cancelled == true).CountAsync();                
        }
        public async Task<int> GetTotalLeaveRequest()
        {
           return await _dbContext.LeaveRequests.CountAsync();                
        }
        public async Task<int> GetPendingLeaveRequest()
        {
           return await _dbContext.LeaveRequests.Where(x=> x.Approved == false).CountAsync();                
        }
        public async Task<int> GetApprovedLeaveRequest()
        {
           return await _dbContext.LeaveRequests.Where(x=>x.Approved == true).CountAsync();                
        }
        public async Task<int> GetRejectedLeaveRequest()
        {
           return await _dbContext.LeaveRequests.Where(x=>x.Cancelled == true).CountAsync();                
        }
    }
}
