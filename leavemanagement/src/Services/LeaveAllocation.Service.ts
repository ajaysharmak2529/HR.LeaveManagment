import { api } from "../Redux/Api";
import { ApiResponse, PageList } from "../Types/ApiResponse";
import { LeaveAloocationType, CreateLeaveAllocation, AdminLeaveAloocationType } from "../Types/LeaveAllocation.Type";

const LeaveAllocationService = api.injectEndpoints({
    endpoints: (builder) => ({
        getLeaveAllocarions: builder.query<ApiResponse<PageList<LeaveAloocationType[]>>, { page: number, pageSize: number }>({
            query: (query) => `/LeaveAllocation/GetAll?page=${query.page}&pageSize=${query.pageSize}`,
            providesTags: ['LeaveAllocation']
        }),
        getEmployeeLeaveAllocarions: builder.query<ApiResponse<PageList<LeaveAloocationType[]>>, { page: number, pageSize: number }>({
            query: (query) => `/LeaveAllocation/Employee?page=${query.page}&pageSize=${query.pageSize}`,
            providesTags: ['LeaveAllocation', "LeaveRequest"]
        }),
        getAdminLeaveAllocarions: builder.query<ApiResponse<PageList<AdminLeaveAloocationType[]>>, { page: number, pageSize: number }>({
            query: (query) => `/LeaveAllocation/Admin?page=${query.page}&pageSize=${query.pageSize}`,
            providesTags: ['LeaveAllocation']
        }),
        getAllocation: builder.query<ApiResponse<LeaveAloocationType>, number>({
            query: (id) => `/LeaveAllocation/${id}/Get`,
        }),
        addAllocation: builder.mutation<ApiResponse<string>, CreateLeaveAllocation>({
            query: (body) => ({
                url: '/LeaveAllocation/Create',
                method: 'POST',
                body
            }),
            invalidatesTags: ['LeaveAllocation']
        }),
        updateAllocation: builder.mutation<ApiResponse<string>, any>({
            query: (body) => ({
                url: '/LeaveAllocation/Update',
                method: 'PUT',
                body
            }),
            invalidatesTags: ["LeaveAllocation"]
        }),
        deleteAllocation: builder.mutation<ApiResponse<string>, number>({
            query: (id) => ({
                url: `/LeaveAllocation/${id}/Delete`,
                method: 'DELETE',
            }),
            invalidatesTags: ['LeaveAllocation']
        })
    })
})

export const {
    useAddAllocationMutation,
    useDeleteAllocationMutation,
    useGetAllocationQuery,
    useGetLeaveAllocarionsQuery,
    useUpdateAllocationMutation,
    useGetEmployeeLeaveAllocarionsQuery,
    useGetAdminLeaveAllocarionsQuery
} = LeaveAllocationService;