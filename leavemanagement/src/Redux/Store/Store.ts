import { configureStore } from '@reduxjs/toolkit';
import { api } from '../Api';
import LogedInUser from '../Slices/LogedInUserSlice';
import Sidebar from '../Slices/Sidebar.Slice';
import ModalSlice from '../Slices/Modal.Slice';

export const store = configureStore({
    reducer: {
        [api.reducerPath]: api.reducer,
        [LogedInUser.name]: LogedInUser.reducer,
        [Sidebar.name]: Sidebar.reducer,
        [ModalSlice.name]: ModalSlice.reducer,
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(api.middleware),
})
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;