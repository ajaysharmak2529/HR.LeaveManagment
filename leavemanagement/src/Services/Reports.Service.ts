import { api } from "../Redux/Api";
import { ApiResponse } from "../Types/ApiResponse";
import { Report } from "../Types/Report.Type";


const ReportService = api.injectEndpoints({
    endpoints: (builder) => ({
        employeeReport: builder.query<ApiResponse<Report>, string>({
            query: () =>"/Reports/EmployeeReport"
        }),
        adminReport: builder.query<ApiResponse<Report>,string>({
            query: () =>"/Reports/AdminReport"
        })
    })
})
export const { useAdminReportQuery, useEmployeeReportQuery } = ReportService;