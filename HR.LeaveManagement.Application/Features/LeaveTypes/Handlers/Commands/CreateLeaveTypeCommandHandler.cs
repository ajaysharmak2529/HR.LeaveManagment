using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLeaveTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            try
            {
                var validate = new CreateLeaveTypeDtoValidator();
                var validationResult = await validate.ValidateAsync(request.LeaveTypeDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    response.Success = false;
                    response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    response.Message = "Validation failed";
                    return response;
                }

                var leaveType = _mapper.Map<Domain.LeaveType>(request.LeaveTypeDto);
                leaveType = await _unitOfWork.LeaveTypes.AddAsync(leaveType);
                await _unitOfWork.SaveChangesAsync();

                response.Success = true;
                response.Id = leaveType.Id;
                response.Message = "Leave Type Created Successfully";
            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Errors?.Add(ex.Message);
            }
            return response;

        }
    }
}
