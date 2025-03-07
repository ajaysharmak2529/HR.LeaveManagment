using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace HR.LeaveManagement.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<BaseResult<Employee>> GetEmployee(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var employee =  new Employee
            {
                Email = user?.Email!,
                Id = user?.Id!,
                FirstName = user?.FirstName!,
                LastName = user?.LastName!
            };
            return BaseResult<Employee>.Success(employee,string.Empty);
        }
        public async Task<BaseResult<List<Employee>>> GetEmployees()
        {
            var users = await _userManager.GetUsersInRoleAsync("Employee");
            var  employees = users.Select(q => new Employee
            {
                Id = q.Id,
                Email = q.Email!,
                FirstName = q.FirstName,
                LastName = q.LastName
            }).ToList();
            return BaseResult<List<Employee>>.Success(employees, string.Empty);
        }
    }
}
