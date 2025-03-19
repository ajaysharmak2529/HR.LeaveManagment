import { api } from "../Redux/Api";
import { ApiResponse } from "../Types/ApiResponse";
import { Report } from "../Types/Report.Type";


const ReportService = api.injectEndpoints({
    endpoints: (builder) => ({
        employeeReport: builder.query<ApiResponse<Report>, string>({
            query: () => "/Reports/EmployeeReport",
            providesTags: ["LeaveRequest"]
        }),
        adminReport: builder.query<ApiResponse<Report>,string>({
            query: () => "/Reports/AdminReport",
            providesTags: ["LeaveRequest"]
        })
    })
})
export const { useAdminReportQuery, useEmployeeReportQuery } = ReportService;