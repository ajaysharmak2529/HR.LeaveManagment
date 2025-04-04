﻿using AutoMapper;
using HR.LeaveManagement.Application.Constants;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using HR.LeaveManagement.Application.Helpers;
using HR.LeaveManagement.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Queries;

public class GetEmployeeLeaveRequestListRequestHandler : IRequestHandler<GetEmployeeLeaveRequestListRequest, PageList<LeaveRequestListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly HttpRequest _request;

    public GetEmployeeLeaveRequestListRequestHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _request = httpContextAccessor.HttpContext.Request;
    }
    public async Task<PageList<LeaveRequestListDto>> Handle(GetEmployeeLeaveRequestListRequest request, CancellationToken cancellationToken)
    {
        string? userId = await _request.GetTokenClaims(CustomClaimTypes.Uid);

        var list = await _unitOfWork.LeaveRequests.GetAllEmployeeLeaveRequestsWithDetailAsync(userId!, request.Page!.Value, request.PageSize!.Value, request.Status);

        var mappedList = _mapper.Map<List<LeaveRequestListDto>>(list.Items);
        return new PageList<LeaveRequestListDto>(mappedList, list.Page, list.PageSize, list.TotalCount);

    }
}
