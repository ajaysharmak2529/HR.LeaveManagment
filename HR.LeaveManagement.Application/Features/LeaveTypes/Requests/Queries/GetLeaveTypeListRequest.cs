using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Models;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries
{
    public class GetLeaveTypeListRequest : IRequest<PageList<LeaveTypeDto>>
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
