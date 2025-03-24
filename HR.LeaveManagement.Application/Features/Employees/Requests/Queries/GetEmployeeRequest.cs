using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Application.Models.Identity;
using MediatR;

namespace HR.LeaveManagement.Application.Features.Employees.Requests.Queries
{
    public class GetEmployeeRequest : IRequest<BaseResult<Employee>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
