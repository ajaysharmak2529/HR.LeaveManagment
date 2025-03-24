using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Models;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;

public class GetEmployeeLeaveRequestListRequest: IRequest<PageList<LeaveRequestListDto>>
{
    public int? Page { get; set; }
    public int? PageSize { get; set; }
}
