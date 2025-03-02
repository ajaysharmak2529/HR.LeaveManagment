import { createSlice, PayloadAction } from "@reduxjs/toolkit"
import { ILogedInUser, ILogedInUserSlice } from "../../Types/LogedInUser"
import secureLocalStorage from "react-secure-storage"

const initialState: ILogedInUser = {
    id: 0,
    email: "",
    userName:"",
    firstName: "",
    lastName: "",
    accessToken: "",
    refeshToken: new String(secureLocalStorage.getItem("refreshToken")) ?? "",
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
            state.firstName = action.payload.firstName;
            state.lastName = action.payload.lastName;
            state.accessToken = action.payload.accessToken;
            state.refeshToken = action.payload.refeshToken;
            secureLocalStorage.setItem("refreshToken", action.payload.refeshToken!)
        },
        logout: (state) => {
            state.id = 0;
            state.email = null;
            state.userName = null;
            state.firstName = null;
            state.lastName = null;
            state.accessToken = null;
            state.refeshToken = null;
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