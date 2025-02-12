using AutoMapper;
using HR.LeaveManagement.Application.Contracts;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public CreateLeaveRequestCommandHandler(IUnitOfWork unitOfWork,IEmailSender emailSender, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validate = new CreateLeaveRequestDtoValidator(_unitOfWork.LeaveTypes);
            var validationResult = await validate.ValidateAsync(request.LeaveRequestDto, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                response.Success = false;
                response.Message = "Failed to validate";
                return response;
            }

            var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request.LeaveRequestDto);
            leaveRequest = await _unitOfWork.LeaveRequests.AddAsync(leaveRequest);

            response.Success = true;
            response.Message = "Leave Request Created Successfully";
            response.Id = leaveRequest.Id;

            var email = new Email
            {
                Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} is pending approval",
                Subject = "Leave Request Application",
                To = "admin@localhost"
            };
            try
            {
              await  _emailSender.SendEmailAsync(email);
            }
            catch (System.Exception ex)
            {
                // Log error or handle exception, but don't throw
            }

            return response;
        }
    }
}
