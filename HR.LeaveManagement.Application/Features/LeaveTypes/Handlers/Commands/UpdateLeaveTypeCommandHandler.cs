using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validate = new UpdateLeaveTypeDtoValidator();
            var validationResult = await validate.ValidateAsync(request.LeaveTypeDto, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var leaveType = await _unitOfWork.LeaveTypes.GetAsync(request.LeaveTypeDto.Id);

            if (leaveType == null)
                throw new NotFoundException(nameof(LeaveType), request.LeaveTypeDto.Id);

            _mapper.Map(request.LeaveTypeDto, leaveType);
            await _unitOfWork.LeaveTypes.UpdateAsync(leaveType);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
