using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse() { Success = false };
            try
            {

                var validate = new UpdateLeaveTypeDtoValidator();
                var validationResult = await validate.ValidateAsync(request.LeaveTypeDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    response.Success = false;
                    response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                    return response;
                }

                var leaveType = await _unitOfWork.LeaveTypes.GetAsync(request.LeaveTypeDto.Id);

                if (leaveType == null)
                {
                    response.Success = false;
                    response.Errors = new List<string> {"Leave not found."};
                    response.Message = "Leave-type not found.";
                    return response;
                }

                _mapper.Map(request.LeaveTypeDto, leaveType);
                await _unitOfWork.LeaveTypes.UpdateAsync(leaveType);
                await _unitOfWork.SaveChangesAsync();

                response.Success = true;;
                response.Message = "Leave-type updated successfully";
            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
            return response;
        }
    }
}
