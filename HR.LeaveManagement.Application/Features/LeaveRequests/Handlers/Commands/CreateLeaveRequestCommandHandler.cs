using AutoMapper;
using HR.LeaveManagement.Application.Constants;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Helpers;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands;
public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _context;
    private readonly IUserService _userService;
    private readonly JwtSetting jwtOptions;

    public CreateLeaveRequestCommandHandler(IUnitOfWork unitOfWork,
        IEmailSender emailSender,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IOptionsMonitor<JwtSetting> jwtOptions,
        IUserService userService
        )
    {
        _unitOfWork = unitOfWork;
        _emailSender = emailSender;
        _mapper = mapper;
        _context = httpContextAccessor;
        _userService = userService;
        this.jwtOptions = jwtOptions.CurrentValue;
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

        if (!string.IsNullOrEmpty(_context.HttpContext.Request.Headers["Authorization"].ToString()))
        {
            var userId = await _context.HttpContext.Request.GetTokenClaims(CustomClaimTypes.Uid);

            leaveRequest.EmployeeId = userId!;
            leaveRequest.Approved = false;
            leaveRequest.CreatedBy = userId!;
            leaveRequest = await _unitOfWork.LeaveRequests.AddAsync(leaveRequest);
            await _unitOfWork.SaveChangesAsync();

            response.Id = leaveRequest.Id;
            response.Success = true;
            response.Message = "Leave Request Created Successfully";
            var result = await _userService.GetEmployee(userId!);
            if (result.IsSuccess)
            {
                var email = new Email
                {
                    Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} is pending approval",
                    Subject = "Leave Request Application",
                    To = result.Data?.Email!,
                    IsHtml = false,
                };
                try
                {
                    await _emailSender.SendEmailAsync(email);
                }
                catch (System.Exception ex)
                {
                    
                }
            }

        }
        return response;
    }
}
