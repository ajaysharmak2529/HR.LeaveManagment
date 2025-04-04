﻿using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<PageList<LeaveRequest>> GetAllLeaveRequestsWithDetailAsync(int page, int pageSize, string status)
        {
            var exp = GetWhereCondition(status);

            var query = _dbContext.LeaveRequests.Where(exp)
                .Include(x=>x.LeaveType)
                .OrderByDescending(x => x.DateCreated);

            return await PageList<LeaveRequest>.CreateAsync(query, page, pageSize);
        }
        public async Task<PageList<LeaveRequest>> GetAllPendingLeaveRequestsWithDetailAsync(int page, int pageSize)
        {
            var query = _dbContext.LeaveRequests.Where(x=> x.Cancelled == false && x.Approved == false)
                .Include(x=>x.LeaveType)
                .OrderByDescending(x => x.DateCreated);
            return await PageList<LeaveRequest>.CreateAsync(query, page, pageSize);
        }
        public async Task<PageList<LeaveRequest>> GetAllEmployeeLeaveRequestsWithDetailAsync(string userId, int page, int pageSize, string status)
        {
          var exp =  GetWhereCondition(status);

            var query = _dbContext.LeaveRequests
                .Where(x=>x.EmployeeId == userId).Where(exp)
                .Include(x=>x.LeaveType)
                .OrderByDescending(x => x.DateCreated);

            return await PageList<LeaveRequest>.CreateAsync(query, page, pageSize);
        }
        public async Task<LeaveRequest> GetLeaveRequestWithDetailAsync(int id)
        {
            var leaveRequest = await _dbContext.LeaveRequests
                .Include(x => x.LeaveType)
                .FirstOrDefaultAsync(x => x.Id == id);

            return leaveRequest!;
        }
        public async Task<int> GetEmployeeTotalLeaveRequest(string userId)
        {
           return await _dbContext.LeaveRequests.Where(x=>x.EmployeeId == userId).CountAsync();                
        }
        public async Task<int> GetEmployeePendingLeaveRequest(string userId)
        {
           return await _dbContext.LeaveRequests.Where(x=>x.EmployeeId == userId && x.Approved == false && x.Cancelled == false).CountAsync();                
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
           return await _dbContext.LeaveRequests.CountAsync();  //.Where(x=>x.Cancelled == false && x.Approved == false)            
        }
        public async Task<int> GetPendingLeaveRequest()
        {
           return await _dbContext.LeaveRequests.Where(x=> x.Approved == false & x.Cancelled == false).CountAsync();                
        }
        public async Task<int> GetApprovedLeaveRequest()
        {
           return await _dbContext.LeaveRequests.Where(x=>x.Approved == true).CountAsync();                
        }
        public async Task<int> GetRejectedLeaveRequest()
        {
           return await _dbContext.LeaveRequests.Where(x=>x.Cancelled == true).CountAsync();                
        }

        public static Expression<Func<LeaveRequest, bool>> GetWhereCondition(string status)
        {
            return status switch
            {
                "Pending" => x => x.Approved == false && x.Cancelled == false,
                "Cancelled" => x => x.Approved == false && x.Cancelled == true,
                "Approved" => x => x.Approved == true && x.Cancelled == false,
                _ => x => true
            };
        }
    }
}
