import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react"
import { RootState } from "../Redux/Store/Store";

const baseUrl = import.meta.env.VITE_BASE_API_URL ?? "http://localhost:5000/api"

const baseQuery = fetchBaseQuery({
    baseUrl: baseUrl,
    prepareHeaders: (headers, { getState }) =>
    {
        const { accessToken } = (getState() as RootState).LogedInUser;

        if (accessToken !== "") {
            headers.set('Authorization', `Bearer ${accessToken}`);
        }
        headers.set('Content-Type', 'application/json');
        headers.set('Accept', 'application/json');
        return headers;
    }
})

export const api = createApi({
    reducerPath: "api",
    baseQuery: baseQuery,
    endpoints: () => ({}),
    tagTypes:["LeaveType","LeaveRequest","LeaveAllocation"]
})