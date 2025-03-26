using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using HR.LeaveManagement.Application.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Queries
{
    public class GetAdminAllocationListRequestHandler : IRequestHandler<GetAdminAllocationListRequest, PageList<AllocationGroupResultDto>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAdminAllocationListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<PageList<AllocationGroupResultDto>> Handle(GetAdminAllocationListRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocationsList = await unitOfWork.LeaveAllocations.GetAllAdminLeaveAllocationsWithDetailAsync( request.Page!.Value, request.PageSize!.Value);


            return new PageList<AllocationGroupResultDto>(leaveAllocationsList.Items!, leaveAllocationsList.Page,leaveAllocationsList.PageSize,leaveAllocationsList.TotalCount);
        }
    }
}
