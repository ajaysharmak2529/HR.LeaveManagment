using HR.LeaveManagement.Application.Models.Identity;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Register(RegistrationRequest request);
        Task<AuthResponse> Login(AuthRequest request);
        Task LogOut();
        Task<AuthResponse> RefreshUserToken(string refreshToken);
    }
}
