import { api } from "../Redux/Api";
import { ApiResponse, PageList } from "../Types/ApiResponse";
import { LeaveType, CreateLeaveTypeRequest } from "../Types/LeaveType.Type";

const LeaveTypeService = api.injectEndpoints({
    endpoints: (builder) => ({
        getLeaveTypes: builder.query<ApiResponse<PageList<LeaveType[]>>, { page: number, pageSize: number }>({
            query: (query) => `/LeaveType/GetAll?page=${query.page}&pageSize=${query.pageSize}`,
            providesTags: ["LeaveType"]
        }),
        getLeaveType: builder.mutation<ApiResponse<LeaveType>, number>({
            query: (id) => ({
                url: `/LeaveType/${id}/Get`,
                method: "GET"
            }),
        }),
        addLeaveType: builder.mutation<ApiResponse<string>, CreateLeaveTypeRequest>({
            query: (body) => ({
                url: '/LeaveType/Create',
                method: 'POST',
                body
            }),
            invalidatesTags: ["LeaveType"]
        }),
        updateLeaveType: builder.mutation<ApiResponse<string>, LeaveType>({
            query: (body) => ({
                url: '/LeaveType/Update',
                method: 'PUT',
                body
            }),
            invalidatesTags: ["LeaveType"]
        }),
        deleteLeaveType: builder.mutation<ApiResponse<string>, number>({
            query: (id) => ({
                url: `/LeaveType/${id}/Delete`,
                method: 'DELETE',
            }),
            invalidatesTags: ["LeaveType"]
        })
    })
})

export const { useAddLeaveTypeMutation, useDeleteLeaveTypeMutation, useGetLeaveTypeMutation, useGetLeaveTypesQuery, useUpdateLeaveTypeMutation } = LeaveTypeService;
