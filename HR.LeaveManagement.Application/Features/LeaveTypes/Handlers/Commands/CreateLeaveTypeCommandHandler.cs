using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validate = new CreateLeaveTypeDtoValidator();
            var validationResult = await validate.ValidateAsync(request.LeaveTypeDto, cancellationToken);

            if (!validationResult.IsValid)
                throw new System.Exception(string.Join(',', validationResult.Errors.Select(x=>x.ErrorMessage)));

            var leaveType = _mapper.Map<Domain.LeaveType>(request.LeaveTypeDto);
            leaveType = await _leaveTypeRepository.AddAsync(leaveType);
            return leaveType.Id;
        }
    }
}
