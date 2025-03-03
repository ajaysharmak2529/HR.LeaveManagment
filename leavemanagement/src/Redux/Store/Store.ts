import { configureStore } from '@reduxjs/toolkit';
import { api } from '../Api';
import LogedInUser from '../Slices/LogedInUserSlice';

export const store = configureStore({
    reducer: {
        [api.reducerPath]: api.reducer,
        [LogedInUser.name]: LogedInUser.reducer
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(api.middleware),
})