import { api } from "../Redux/Api";

const LeaveTypeService = api.injectEndpoints({
    endpoints: (builder) => ({
        getLeaveTypes: builder.query({
            query: () => '/LeaveType'
        }),
        addLeaveType: builder.mutation({
            query: (body) => ({
                url: '/LeaveType',
                method: 'POST',
                body
            })
        }),
        updateLeaveType: builder.mutation({
            query: (body) => ({
                url: '/LeaveType',
                method: 'PUT',
                body
            })
        }),
        getLeaveType: builder.query({
            query: (id: number) => `/LeaveType/${id}`
        }),
        deleteLeaveType: builder.mutation({
            query: (id: number) => ({
                url: `/LeaveType/${id}`,
                method: 'DELETE',
            })
        })
    })
})

export const { useAddLeaveTypeMutation, useDeleteLeaveTypeMutation, useGetLeaveTypeQuery, useGetLeaveTypesQuery, useUpdateLeaveTypeMutation } = LeaveTypeService;
