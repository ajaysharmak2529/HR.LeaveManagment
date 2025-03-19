using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.Reports;
using HR.LeaveManagement.Application.Features.Reports.Requests;
using HR.LeaveManagement.Application.Models;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

namespace HR.LeaveManagement.Application.Features.Reports.Handlers
{
    public class AdminReportRequestHandler :IRequestHandler<AdminReportRequest, BaseResult<ReportDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminReportRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResult<ReportDto>> Handle(AdminReportRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var Rejected = await _unitOfWork.LeaveRequests.GetRejectedLeaveRequest();
                var Approved = await _unitOfWork.LeaveRequests.GetApprovedLeaveRequest();
                var Pending = await _unitOfWork.LeaveRequests.GetPendingLeaveRequest();
                var TotalLeaveRequest = await _unitOfWork.LeaveRequests.GetTotalLeaveRequest();

                return BaseResult<ReportDto>.Success(new ReportDto { Approved = Approved, Pending = Pending, Rejected = Rejected, TotalRequests = TotalLeaveRequest }, "Report generated successfully.");
            }
            catch (System.Exception ex)
            {
                return BaseResult<ReportDto>.Fail(ex.Message);
            }
        }
    }
}
