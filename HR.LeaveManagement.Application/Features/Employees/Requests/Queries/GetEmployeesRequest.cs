using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Application.Models.Identity;
using MediatR;

namespace HR.LeaveManagement.Application.Features.Employees.Requests.Queries
{
    public class GetEmployeesRequest : IRequest<BaseResult<PageList<Employee>>>
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
