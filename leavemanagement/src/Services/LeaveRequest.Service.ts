import { api } from "../Redux/Api";
import { ApiResponse } from "../Types/ApiResponse";
import { CreateLeaveRequest, LeaveRequestType, UpdateLeaveRequest } from "../Types/LeaveRequest.Type";

const LeaveRequestService = api.injectEndpoints({
    endpoints: (builder) => ({
        getLeaveRequests: builder.query<ApiResponse<LeaveRequestType[]>, string>({
            query: () => '/LeaveRequest/GetAll'
        }),
        getLeaveRequest: builder.query<ApiResponse<LeaveRequestType>, number>({
            query: (id) => `/LeaveRequest/${id}/Get`
        }),
        addLeaveRequest: builder.mutation<ApiResponse<string>, CreateLeaveRequest>({
            query: (body) => ({
                url: '/LeaveRequest/Create',
                method: 'POST',
                body,                
            })
        }),
        updateLeaveRequest: builder.mutation<ApiResponse<string>, UpdateLeaveRequest>({
            query: (body) => ({
                url: '/LeaveRequest/Update',
                method: 'PUT',
                body
            })
        }),
        deleteLeaveRequest: builder.mutation<ApiResponse<string>, number>({
            query: (id) => ({
                url: `/LeaveRequest/${id}/Delete`,
                method: 'DELETE',
            })
        })
    })
});

export const { useAddLeaveRequestMutation, useDeleteLeaveRequestMutation, useGetLeaveRequestQuery, useGetLeaveRequestsQuery, useUpdateLeaveRequestMutation } = LeaveRequestService;