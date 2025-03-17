using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveRequestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            try
            {
                var leaveRequest = await _unitOfWork.LeaveRequests.GetAsync(request.LeaveRequestDto != null ? request.LeaveRequestDto.Id : request.LeaveRequestApprovalDto.Id);

                if (leaveRequest is null)
                {
                    response.Success = false;
                    response.Message = "Not found request";
                    response.Errors = new System.Collections.Generic.List<string> { "Not found request" };
                }
                else
                {
                    if (request.LeaveRequestApprovalDto != null)
                    {
                        _mapper.Map(request.LeaveRequestDto, leaveRequest);
                        await _unitOfWork.LeaveRequests.ChangeLeaveRequestApproval(leaveRequest, request.LeaveRequestApprovalDto.Approved);
                    }
                    else if (request.LeaveRequestDto != null)
                    {
                        var validate = new UpdateLeaveRequestDtoValidator(_unitOfWork.LeaveTypes);
                        var validationResult = await validate.ValidateAsync(request.LeaveRequestDto, cancellationToken);

                        if (!validationResult.IsValid)
                            throw new ValidationException(validationResult);

                        await _unitOfWork.LeaveRequests.UpdateAsync(leaveRequest);
                    }
                    response.Success = true;
                    response.Message = "Updated successfully";
                    await _unitOfWork.SaveChangesAsync();
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
