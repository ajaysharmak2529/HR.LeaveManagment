import { api } from "../Redux/Api";
import { ApiResponse } from "../Types/ApiResponse";
import { LeaveType, CreateLeaveTypeRequest, UpdateLeaveTypeRequest } from "../Types/LeaveType.Type";

const LeaveTypeService = api.injectEndpoints({
    endpoints: (builder) => ({
        getLeaveTypes: builder.query<ApiResponse<LeaveType[]>, string>({
            query: () => '/LeaveType/GetAll'
        }),
        getLeaveType: builder.query<ApiResponse<LeaveType>, number>({
            query: (id) => `/LeaveType/${id}/Get`
        }),
        addLeaveType: builder.mutation<ApiResponse<string>, CreateLeaveTypeRequest>({
            query: (body) => ({
                url: '/LeaveType/Create',
                method: 'POST',
                body
            })
        }),
        updateLeaveType: builder.mutation<ApiResponse<string>, UpdateLeaveTypeRequest>({
            query: (body) => ({
                url: '/LeaveType/Update',
                method: 'PUT',
                body
            })
        }),
        deleteLeaveType: builder.mutation<ApiResponse<string>, number>({
            query: (id) => ({
                url: `/LeaveType/${id}/Delete`,
                method: 'DELETE',
            })
        })
    })
})

export const { useAddLeaveTypeMutation, useDeleteLeaveTypeMutation, useGetLeaveTypeQuery, useGetLeaveTypesQuery, useUpdateLeaveTypeMutation } = LeaveTypeService;
