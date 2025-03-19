using HR.LeaveManagement.Application.Constants;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.Reports;
using HR.LeaveManagement.Application.Features.Reports.Requests;
using HR.LeaveManagement.Application.Helpers;
using HR.LeaveManagement.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.Reports.Handlers
{
    public class EmployeeReportRequestHander : IRequestHandler<EmployeeReportRequest, BaseResult<ReportDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpRequest _request;

        public EmployeeReportRequestHander(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _request = httpContextAccessor.HttpContext.Request;
        }
        public async Task<BaseResult<ReportDto>> Handle(EmployeeReportRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = await _request.GetTokenClaims(CustomClaimTypes.Uid);

                var Rejected = await _unitOfWork.LeaveRequests.GetEmployeeRejectedLeaveRequest(userId!);
                var Approved = await _unitOfWork.LeaveRequests.GetEmployeeApprovedLeaveRequest(userId!);
                var Pending = await _unitOfWork.LeaveRequests.GetEmployeePendingLeaveRequest(userId!);
                var TotalLeaveRequest = await _unitOfWork.LeaveRequests.GetEmployeeTotalLeaveRequest(userId!);

                return BaseResult<ReportDto>.Success(new ReportDto { Approved = Approved, Pending = Pending,Rejected = Rejected,TotalRequests= TotalLeaveRequest },"Report generated successfully.");
            }
            catch (System.Exception ex)
            {
                return BaseResult<ReportDto>.Fail(ex.Message);
            }
        }
    }
}
