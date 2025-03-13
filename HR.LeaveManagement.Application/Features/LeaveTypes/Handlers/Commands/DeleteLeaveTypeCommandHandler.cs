using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeRequestCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLeaveTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseCommandResponse> Handle(DeleteLeaveTypeRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            try
            {
                var leaveType = await _unitOfWork.LeaveTypes.GetAsync(request.Id);

                if (leaveType == null)
                {
                    response.Success = false;
                    response.Message = "Not found";
                    response.Errors = new System.Collections.Generic.List<string> { "Not found" };
                }
                else
                {
                    await _unitOfWork.LeaveTypes.DeleteAsync(leaveType);
                    await _unitOfWork.SaveChangesAsync();
                    response.Success = true;
                    response.Message = "Deleted successfully";
                }
            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message = "Exception";
                response.Errors = new System.Collections.Generic.List<string> { ex.Message };
            }
            return response;
        }
    }
}
