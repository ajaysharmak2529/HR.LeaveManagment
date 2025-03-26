using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Models;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries
{
    public class GetAdminAllocationListRequest : IRequest<PageList<AllocationGroupResultDto>>
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
