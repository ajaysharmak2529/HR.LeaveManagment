using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Application.Models.Identity;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<BaseResult<AuthResponse>> Register(RegistrationRequest request);
        Task<BaseResult<AuthResponse>> Login(AuthRequest request);
        Task LogOut();
        Task<BaseResult<AuthResponse>> RefreshUserToken(string refreshToken);
    }
}
