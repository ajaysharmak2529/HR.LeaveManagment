import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react"

const baseUrl = import.meta.env.VITE_BASE_API_URL ?? "http://localhost:5000/api"

const baseQuery = fetchBaseQuery({
    baseUrl: baseUrl,
    prepareHeaders: (headers) =>
    {
        headers.set('Content-Type', 'application/json');
        headers.set('Accept', 'application/json');
        return headers;
    }
})

export const api = createApi({
    reducerPath: "api",
    baseQuery: baseQuery,
    endpoints: () => ({})
})