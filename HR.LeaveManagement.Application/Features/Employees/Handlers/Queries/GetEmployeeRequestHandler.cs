using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Features.Employees.Requests.Queries;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Application.Models.Identity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.Employees.Handlers.Queries
{
    public class GetEmployeeRequestHandler : IRequestHandler<GetEmployeeRequest, BaseResult<Employee>>
    {
        private readonly IUserService _userService;

        public GetEmployeeRequestHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<BaseResult<Employee>> Handle(GetEmployeeRequest request, CancellationToken cancellationToken)
        {
            var result = await _userService.GetEmployee(request.UserId);
            return result;
        }
    }
}
