using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetAsync(request.LeaveRequestDto!= null ?request.LeaveRequestDto.Id : request.LeaveRequestApprovalDto.Id);

            if (leaveRequest is null)
                throw new NotFoundException(nameof(LeaveRequest), request?.LeaveRequestDto?.Id!);

            if (request.LeaveRequestApprovalDto != null)
            {
                _mapper.Map(request.LeaveRequestDto, leaveRequest);
                await _leaveRequestRepository.ChangeLeaveRequestApproval(leaveRequest,request.LeaveRequestApprovalDto.Approved);
            }
            else if (request.LeaveRequestDto != null)
            {
                var validate = new UpdateLeaveRequestDtoValidator(_leaveTypeRepository);
                var validationResult = await validate.ValidateAsync(request.LeaveRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                    throw new ValidationException(validationResult);

                await _leaveRequestRepository.UpdateAsync(leaveRequest);
            }
            return Unit.Value;
        }
    }
}
