import { api } from '../Redux/Api';
import { ApiResponse } from '../Types/ApiResponse';
import { ILogedInUserSlice, ILogin, ISignUp } from '../Types/LogedInUser';


export const authService = api.injectEndpoints({
    endpoints: (builder) => ({
        login: builder.mutation<ApiResponse<ILogedInUserSlice>, ILogin>({
            query: (body) => ({
                url: '/auth/login',
                method: 'POST',
                body
            })
        }),
        signup: builder.mutation<ApiResponse<ILogedInUserSlice>, ISignUp>({
            query: (body) => ({
                url: '/auth/register',
                method: 'POST',
                body
            })
        }),
        refreshToken: builder.mutation<ApiResponse<ILogedInUserSlice>, string>({
            query: (token) => ({
                url: `/auth/refresh?refreshToken=${token}`,
                method: 'POST',
            })
        }),
    })
})

export const { useLoginMutation, useSignupMutation, useRefreshTokenMutation } = authService;