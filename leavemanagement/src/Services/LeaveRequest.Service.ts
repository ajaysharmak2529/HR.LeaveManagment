import { api } from "../Redux/Api";

const LeaveRequestService = api.injectEndpoints({
    endpoints: (builder) => ({
        getLeaveRequests: builder.query({
            query: () => '/LeaveRequest'
        }),
        addLeaveRequest: builder.mutation({
            query: (body) => ({
                url: '/LeaveRequest',
                method: 'POST',
                body
            })
        }),
        updateLeaveRequest: builder.mutation({
            query: (body) => ({
                url: '/LeaveRequest',
                method: 'PUT',
                body
            })
        }),
        getLeaveRequest: builder.query({
            query: (id: number) => `/LeaveRequest/${id}`
        }),
        deleteLeaveRequest: builder.mutation({
            query: (id: number) => ({
                url: `/LeaveRequest/${id}`,
                method: 'DELETE',
            })
        })
    })
});

export const { useAddLeaveRequestMutation, useDeleteLeaveRequestMutation, useGetLeaveRequestQuery, useGetLeaveRequestsQuery, useUpdateLeaveRequestMutation } = LeaveRequestService;