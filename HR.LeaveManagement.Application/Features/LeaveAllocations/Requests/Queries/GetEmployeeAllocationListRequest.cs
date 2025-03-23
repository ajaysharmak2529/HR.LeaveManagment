using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Models;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries
{
    public class GetEmployeeAllocationListRequest :IRequest<PageList<LeaveAllocationDto>>
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
