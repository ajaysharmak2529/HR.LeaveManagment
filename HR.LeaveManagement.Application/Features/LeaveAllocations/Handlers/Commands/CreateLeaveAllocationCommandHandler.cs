using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly ILogger<CreateLeaveAllocationCommandHandler> logger;

        public CreateLeaveAllocationCommandHandler(
            IUnitOfWork unitOfWork,
            IUserService userService,
            ILogger<CreateLeaveAllocationCommandHandler> logger
            )
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            this.logger = logger;
        }
        public async Task<BaseCommandResponse> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            try
            {
                var validate = new CreateLeaveAllocationDtoValidator(_unitOfWork.LeaveTypes);
                var validationResult = await validate.ValidateAsync(request.LeaveAllocationDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    response.Success = false;
                    response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                    response.Message = "Validation Failed";
                    return response;
                }

                var leaveType = await _unitOfWork.LeaveTypes.GetAsync(request.LeaveAllocationDto.LeaveTypeId);
                var employees = await _userService.GetEmployees();
                var period = DateTime.Now.Year;
                var allocations = new List<LeaveAllocation>();
                foreach (var emp in employees.Data!)
                {
                    if (await _unitOfWork.LeaveAllocations.AllocationExistsAsync(emp.Id, leaveType.Id, period))
                        continue;
                    allocations.Add(new LeaveAllocation
                    {
                        EmployeeId = emp.Id,
                        LeaveTypeId = leaveType.Id,
                        NumberOfDays = leaveType.DefaultDays,
                        Period = period
                    });
                }

                await _unitOfWork.LeaveAllocations.AddAllocationsAsync(allocations);
                await _unitOfWork.SaveChangesAsync();

                logger.LogInformation($"Leave Allocations were created for {allocations.Count} employees");

                response.Success = true;
                response.Message = "Allocations added Successfully";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                response.Success = false;
                response.Message = "An error occurred";
                response.Errors ??= new List<string>();
                response.Errors?.Add(ex.Message);
            }

            return response;
        }
    }
}
