import { api } from '../Redux/Api';


export const authService = api.injectEndpoints({
    endpoints: (builder) => ({
        login: builder.mutation({
            query: (body) => ({
                url: '/auth/login',
                method: 'POST',
                body
            })
        }),
        signup: builder.mutation({
            query: (body) => ({
                url: '/auth/register',
                method: 'POST',
                body
            })
        }),
        refreshToken: builder.mutation({
            query: (token: string) => ({
                url: `/auth/refresh?refreshToken=${token}`,
                method: 'POST',
            })
        }),
    })
})

export const { useLoginMutation, useSignupMutation, useRefreshTokenMutation } = authService;