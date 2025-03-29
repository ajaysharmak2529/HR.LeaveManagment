using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using HR.LeaveManagement.Application.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Queries
{
    public class GetPendingLeaveRequestListRequestHandler : IRequestHandler<GetPendingLeaveRequestListRequest, PageList<LeaveRequestListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPendingLeaveRequestListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PageList<LeaveRequestListDto>> Handle(GetPendingLeaveRequestListRequest request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.LeaveRequests.GetAllPendingLeaveRequestsWithDetailAsync(request.Page!.Value, request.PageSize!.Value);

            var mappedResult = _mapper.Map<List<LeaveRequestListDto>>(result.Items);

            return new PageList<LeaveRequestListDto>(mappedResult, result.Page, result.PageSize, result.TotalCount);
        }
    }
}
