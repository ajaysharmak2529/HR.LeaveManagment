using HR.LeaveManagement.Application.DTOs.Reports;
using HR.LeaveManagement.Application.Features.Reports.Requests;
using HR.LeaveManagement.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ReportsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("EmployeeReport")]
        [Authorize(Roles ="Employee,Admin")]
        public async Task<IActionResult> GetEmployeeReport()
        {
            var result = await mediator.Send(new EmployeeReportRequest());
            if (result.IsSuccess)
            {
                return Ok(ApiResponse<ReportDto>.Success(result.Data!, StatusCodes.Status200OK, "Employee reports generated successfully."));
            }
            else
            {
                return BadRequest(ApiResponse<ReportDto>.Fail(result.Message!,StatusCodes.Status400BadRequest,result.Errors));
            }
        }
        [HttpGet("AdminReport")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAdminReport()
        {
            var result = await mediator.Send(new AdminReportRequest());
            if (result.IsSuccess)
            {
                return Ok(ApiResponse<ReportDto>.Success(result.Data!, StatusCodes.Status200OK, "Employee reports generated successfully."));
            }
            else
            {
                return BadRequest(ApiResponse<ReportDto>.Fail(result.Message!,StatusCodes.Status400BadRequest,result.Errors));
            }
        }
    }
}
