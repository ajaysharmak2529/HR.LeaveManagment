import { api } from "../Redux/Api";

const LeaveAllocationService = api.injectEndpoints({
    endpoints: (builder) => ({
        getLeaveAllocarions: builder.query({
            query: () => '/LaveAllocation'
        }),
        addAllocation: builder.mutation({
            query: (body) => ({
                url: '/LaveAllocation',
                method: 'PAST',
                body
            })
        }),
        updateAllocation: builder.mutation({
            query: (body) => ({
                url: '',
                method: 'PUT',
                body
            })
        }),
        getAllocation: builder.query({
            query: (id: number) => `/LaveAllocation/${id}`
        }),
        deleteAllocation: builder.mutation({
            query: (id:number) => ({
                url: `/LaveAllocation/${id}`,
                method: 'DELETE',

            })
        })
    })
})

export const { useAddAllocationMutation, useDeleteAllocationMutation, useGetAllocationQuery, useGetLeaveAllocarionsQuery, useUpdateAllocationMutation } = LeaveAllocationService;