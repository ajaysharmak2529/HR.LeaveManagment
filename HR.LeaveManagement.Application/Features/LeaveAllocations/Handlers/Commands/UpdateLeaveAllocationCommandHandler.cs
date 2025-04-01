using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateLeaveAllocationCommandHandler> logger;

        public UpdateLeaveAllocationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateLeaveAllocationCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this.logger = logger;
        }
        public async Task<BaseCommandResponse> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            try
            {
                var validate = new UpdateLeaveAllocationDtoValidator(_unitOfWork.LeaveTypes);
                var validationResult = await validate.ValidateAsync(request.LeaveAllocationDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    response.Success = false;
                    response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                    response.Message = "Update Leave Allocation Failed";
                }
                else
                {
                    var leaveAllocation = await _unitOfWork.LeaveAllocations.GetAsync(request.LeaveAllocationDto.Id);
                    _mapper.Map(request.LeaveAllocationDto, leaveAllocation);
                    await _unitOfWork.LeaveAllocations.UpdateAsync(leaveAllocation);
                    await _unitOfWork.SaveChangesAsync();

                    response.Success = true;
                    response.Message = "Leave Allocation Updated Successfully";
                }
            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Errors = new System.Collections.Generic.List<string> { ex.Message };
                response.Message = "Update Leave Allocation Failed";
                logger.LogError("failed to update leave allocation request json {json}", JsonSerializer.Serialize(request.LeaveAllocationDto));
                logger.LogError(ex, ex.Message);
            }
            return response;
        }
    }
}
