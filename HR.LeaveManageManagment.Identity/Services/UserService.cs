using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly LeaveManagementIdentityDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<ApplicationUser> userManager, LeaveManagementIdentityDbContext leaveManagementIdentityDbContext, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _dbContext = leaveManagementIdentityDbContext;
            _roleManager = roleManager;
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

        public async Task<BaseResult<PageList<Employee>>> GetEmployees(int? page, int? pageSize)
        {
            var role = await _roleManager.FindByNameAsync("Employee");
            if (role == null)
            {
                return BaseResult<PageList<Employee>>.Fail("Not Found.", new string[]{ "Role not found"});
            }
            var userIdsQuery = _dbContext.UserRoles
                .Where(ur => ur.RoleId == role.Id)
                .Select(ur => ur.UserId);

            var Query =  _dbContext.Users
                .Where(u => userIdsQuery.Contains(u.Id))
                .OrderBy(u => u.UserName).Select(x=> new Employee
                {
                    Email = x.Email!,
                    FirstName = x.FirstName,
                    Id = x.Id,
                    LastName= x.LastName,
                });
            var data = await PageList<Employee>.CreateAsync(Query, page!.Value, pageSize!.Value);

            return BaseResult<PageList<Employee>>.Success(data, "Employee List");
        }
    }
}
