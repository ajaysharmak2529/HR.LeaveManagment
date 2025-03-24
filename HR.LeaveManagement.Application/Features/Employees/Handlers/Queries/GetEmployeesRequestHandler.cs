using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Features.Employees.Requests.Queries;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Application.Models.Identity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.Employees.Handlers.Queries
{
    internal class GetEmployeesRequestHandler : IRequestHandler<GetEmployeesRequest, BaseResult<PageList<Employee>>>
    {
        private readonly IUserService _userService;

        public GetEmployeesRequestHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<BaseResult<PageList<Employee>>> Handle(GetEmployeesRequest request, CancellationToken cancellationToken)
        {
            return await _userService.GetEmployees(request.Page,request.PageSize);
        }
    }
}
