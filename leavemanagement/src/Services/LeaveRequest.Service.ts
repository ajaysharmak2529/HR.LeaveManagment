import { api } from "../Redux/Api";
import { ApiResponse, PageList } from "../Types/ApiResponse";
import { CreateLeaveRequest, LeaveRequestType, UpdateLeaveRequest, ChangeApprovelLeaveRequest } from "../Types/LeaveRequest.Type";


const LeaveRequestService = api.injectEndpoints({
    endpoints: (builder) => ({
        getAllLeaveRequests: builder.query<ApiResponse<PageList<LeaveRequestType[]>>, { page: number, pageSize: number }>({
            query: (query) => `/LeaveRequest/GetAll?page=${query.page}&pageSize=${query.pageSize}`,
            providesTags: ["LeaveRequest"]
        }),
        getEmployeeLeaveRequests: builder.query<ApiResponse<PageList<LeaveRequestType[]>>, { page: number, pageSize: number }>({
            query: (query) => `/LeaveRequest/Employee?page=${query.page}&pageSize=${query.pageSize}`,
            providesTags: ["LeaveRequest"]
        }),
        getLeaveRequest: builder.query<ApiResponse<LeaveRequestType>, number>({
            query: (id) => `/LeaveRequest/${id}/Get`
        }),
        addLeaveRequest: builder.mutation<ApiResponse<string>, CreateLeaveRequest>({
            query: (body) => ({
                url: '/LeaveRequest/Add',
                method: 'POST',
                body,                 
            }),
            invalidatesTags: ["LeaveRequest"]
        }),
        updateLeaveRequest: builder.mutation<ApiResponse<string>, UpdateLeaveRequest>({
            query: (body) => ({
                url: '/LeaveRequest/Update',
                method: 'PUT',
                body
            }),
            invalidatesTags: ["LeaveRequest"]
        }),
        chnageApprovalLeaveRequest: builder.mutation<ApiResponse<string>, ChangeApprovelLeaveRequest>({
            query: (body) => ({
                url: '/LeaveRequest/ChangeApproval',
                method: 'PUT',
                body
            }),
            invalidatesTags: ["LeaveRequest"]
        }),
        deleteLeaveRequest: builder.mutation<ApiResponse<string>, number>({
            query: (id) => ({
                url: `/LeaveRequest/${id}/Delete`,
                method: 'DELETE',
            }),
            invalidatesTags: ["LeaveRequest"]
        })
    })
});

export const { useAddLeaveRequestMutation, useDeleteLeaveRequestMutation, useGetLeaveRequestQuery, useGetAllLeaveRequestsQuery, useGetEmployeeLeaveRequestsQuery, useUpdateLeaveRequestMutation, useChnageApprovalLeaveRequestMutation } = LeaveRequestService;