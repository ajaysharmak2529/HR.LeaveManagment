using AutoMapper;
using HR.LeaveManagement.Application.Constants;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Helpers;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly HttpRequest _request;

        public UpdateLeaveRequestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
            _request = httpContextAccessor.HttpContext.Request;
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
                        if (request.LeaveRequestApprovalDto.Approved?? false)
                        {
                            var allocation = await _unitOfWork.LeaveAllocations.GetUserAllocationsAsync(leaveRequest.EmployeeId, leaveRequest.LeaveTypeId);
                            int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;

                            allocation.NumberOfDays -= daysRequested;

                            allocation.ModifiedBy = await _request.GetTokenClaims(CustomClaimTypes.Uid)!;
                            await _unitOfWork.LeaveAllocations.UpdateAsync(allocation);
                        }
                    }
                    else if (request.LeaveRequestDto != null)
                    {
                        var validate = new UpdateLeaveRequestDtoValidator(_unitOfWork.LeaveTypes);
                        var validationResult = await validate.ValidateAsync(request.LeaveRequestDto, cancellationToken);

                        if (!validationResult.IsValid)
                            throw new ValidationException(validationResult);

                        leaveRequest.Cancelled = request.LeaveRequestDto.Cancelled;
                        leaveRequest.StartDate = request.LeaveRequestDto.StartDate;
                        leaveRequest.EndDate = request.LeaveRequestDto.EndDate;
                        leaveRequest.RequestComments = request.LeaveRequestDto.RequestComments;
                        leaveRequest.LeaveTypeId = request.LeaveRequestDto.LeaveTypeId;

                        leaveRequest.ModifiedBy = await _request.GetTokenClaims(CustomClaimTypes.Uid)!;
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
