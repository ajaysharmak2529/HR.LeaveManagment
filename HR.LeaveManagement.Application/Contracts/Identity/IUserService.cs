using System.Collections.Generic;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Application.Models.Identity;

namespace HR.LeaveManagement.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<BaseResult<List<Employee>>> GetEmployees();
        Task<BaseResult<PageList<Employee>>> GetEmployees(int? page,int? pageSize);
        Task<BaseResult<Employee>> GetEmployee(string userId);
    }
}
