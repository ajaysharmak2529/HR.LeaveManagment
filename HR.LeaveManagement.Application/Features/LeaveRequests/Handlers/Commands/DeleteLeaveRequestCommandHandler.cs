using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestRequestCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLeaveRequestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseCommandResponse> Handle(DeleteLeaveRequestRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            try
            {
                var leaveRequest = await _unitOfWork.LeaveRequests.GetAsync(request.Id);

                if (leaveRequest is null)
                {
                    response.Success = false;
                    response.Message = "Leave request not found";
                    response.Errors = new System.Collections.Generic.List<string> { "Not found" };
                }
                else
                {
                    await _unitOfWork.LeaveRequests.DeleteAsync(leaveRequest);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message = "Exception.";
                response.Errors = new System.Collections.Generic.List<string> { ex.Message };
            }
            return response;
        }
    }
}
