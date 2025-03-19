using HR.LeaveManagement.Application.DTOs.Reports;
using HR.LeaveManagement.Application.Models;
using MediatR;

namespace HR.LeaveManagement.Application.Features.Reports.Requests
{
    public class EmployeeReportRequest : IRequest<BaseResult<ReportDto>>
    {
    }
}
