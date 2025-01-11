using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Domain;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Persistence.Contracts
{
    public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
    {
    }
}
