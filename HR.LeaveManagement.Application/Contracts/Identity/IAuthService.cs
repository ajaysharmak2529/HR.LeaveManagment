using HR.LeaveManagement.Application.Models.Identity;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<RegistrationResponse> Register(RegistrationRequest request);
        Task<AuthResponse> Login(AuthRequest request);
        Task LogOut();
        Task<RefreshTokenResponse> RefreshUserToken(string refreshToken);
    }
}
