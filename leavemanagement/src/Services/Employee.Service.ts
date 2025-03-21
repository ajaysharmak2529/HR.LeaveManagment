import { api } from "../Redux/Api";
import { ApiResponse } from "../Types/ApiResponse";
import { Employee } from "../Types/Employee.Type";

const EmployeeService = api.injectEndpoints({
    endpoints: (builder) => ({
        getEmployee: builder.query<ApiResponse<Employee>, string>({
            query: (id) => `/Employees/${id}/Get`
        }),
        getEmployees: builder.query<ApiResponse<Employee[]>,string>({
            query: () => `/Employees/GetAll`
        })
    })
});

export const { useGetEmployeeQuery, useGetEmployeesQuery } = EmployeeService;