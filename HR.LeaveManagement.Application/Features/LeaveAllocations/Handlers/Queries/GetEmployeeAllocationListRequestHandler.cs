using AutoMapper;
using HR.LeaveManagement.Application.Constants;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using HR.LeaveManagement.Application.Helpers;
using HR.LeaveManagement.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Queries
{
    public class GetEmployeeAllocationListRequestHandler : IRequestHandler<GetEmployeeAllocationListRequest, PageList<LeaveAllocationDto>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly HttpRequest _request;

        public GetEmployeeAllocationListRequestHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _request = httpContextAccessor.HttpContext.Request;
        }
        public async Task<PageList<LeaveAllocationDto>> Handle(GetEmployeeAllocationListRequest request, CancellationToken cancellationToken)
        {
            var userId = await _request.GetTokenClaims(CustomClaimTypes.Uid);
            var leaveAllocationsList = await unitOfWork.LeaveAllocations.GetAllEmployeeLeaveAllocationsWithDetailAsync(userId!, request.Page!.Value, request.PageSize!.Value);

            var items = mapper.Map<List<LeaveAllocationDto>>(leaveAllocationsList.Items);

            return new PageList<LeaveAllocationDto>(items,leaveAllocationsList.Page,leaveAllocationsList.PageSize,leaveAllocationsList.TotalCount);
        }
    }
}
