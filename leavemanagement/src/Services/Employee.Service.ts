import { api } from "../Redux/Api";
import { ApiResponse, PageList } from "../Types/ApiResponse";
import { Employee } from "../Types/Employee.Type";

const EmployeeService = api.injectEndpoints({
    endpoints: (builder) => ({
        getEmployee: builder.query<ApiResponse<Employee>, string>({
            query: (id) => `/Employees/${id}/Get`
        }),
        getEmployees: builder.query<ApiResponse<PageList<Employee[]>>, { page: number, pageSize: number }>({
            query: (query) => `/Employees/GetAll?page=${query.page}&pageSize=${query.pageSize}`
        })
    })
});

export const { useGetEmployeeQuery, useGetEmployeesQuery } = EmployeeService;