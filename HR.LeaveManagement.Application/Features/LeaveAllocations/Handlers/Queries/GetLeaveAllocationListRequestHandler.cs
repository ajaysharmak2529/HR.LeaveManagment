﻿using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Models;
using System.Collections.Generic;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Queries
{
    public class GetLeaveAllocationListRequestHandler : IRequestHandler<GetLeaveAllocationListRequest, PageList<LeaveAllocationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLeaveAllocationListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PageList<LeaveAllocationDto>> Handle(GetLeaveAllocationListRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocations = await _unitOfWork.LeaveAllocations.GetAllLeaveAllocationsWithDetailAsync(request.Page!.Value,request.PageSize!.Value);

            var items = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations.Items);

            return new PageList<LeaveAllocationDto>(items,leaveAllocations.Page,leaveAllocations.PageSize,leaveAllocations.TotalCount);
        }
    }
}
