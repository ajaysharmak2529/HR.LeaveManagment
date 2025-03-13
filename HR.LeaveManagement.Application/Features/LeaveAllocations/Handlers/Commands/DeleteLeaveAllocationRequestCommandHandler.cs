using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class DeleteLeaveAllocationRequestCommandHandler : IRequestHandler<DeleteLeaveAllocationRequestCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLeaveAllocationRequestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseCommandResponse> Handle(DeleteLeaveAllocationRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            try
            {
                var leaveAllocation = await _unitOfWork.LeaveAllocations.GetAsync(request.Id);

                if (leaveAllocation is null)
                {
                    response.Success = false;
                    response.Message = $"Allocation not found Id:{request.Id}";
                }
                else
                {
                    await _unitOfWork.LeaveAllocations.DeleteAsync(leaveAllocation);
                    await _unitOfWork.SaveChangesAsync();
                    response.Success = true;
                    response.Message = $"Deleted successfully";
                }
            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message = $"throw exception";
                response.Errors = new System.Collections.Generic.List<string> { ex.Message };
            }
            return response;
        }
    }
}
