using HR.LeaveManagement.Application.Models;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(Email email);
    }
}
