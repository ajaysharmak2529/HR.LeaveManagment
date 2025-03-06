import { createSlice, PayloadAction } from "@reduxjs/toolkit"
import { ILogedInUser, ILogedInUserSlice } from "../../Types/LogedInUser"
import secureLocalStorage from "react-secure-storage"

const initialState: ILogedInUser = {
    id: "",
    email: "",
    userName:"",
    accessToken: "",
    refreshToken: secureLocalStorage.getItem("refreshToken")?.toString() ?? "",
    userTheme: localStorage.getItem("theme") ?? "light"
};

const LogedInUserSlice = createSlice({
    name: "LogedInUser",
    initialState,
    reducers: {
        setLogedInUser: (state, action: PayloadAction<ILogedInUserSlice>) => {

            state.id = action.payload.id;
            state.email = action.payload.email;
            state.userName = action.payload.userName;
            state.accessToken = action.payload.accessToken!;
            state.refreshToken = action.payload.refreshToken!;
            secureLocalStorage.setItem("refreshToken", action.payload.refreshToken!)
        },
        logout: (state) => {
            state.id = "";
            state.email = "";
            state.userName = "";
            state.accessToken = "";
            state.refreshToken = "";
            secureLocalStorage.removeItem("refreshToken")
        },
        setTheme: (state, action: PayloadAction<string>) => {
            state.userTheme = action.payload;
            localStorage.setItem("theme", action.payload)
        }
    }
});

export const { setLogedInUser, logout, setTheme } = LogedInUserSlice.actions;

export default LogedInUserSlice;