import { createSlice, PayloadAction } from "@reduxjs/toolkit"
import { ILogedInUser, ILogedInUserSlice } from "../../Types/LogedInUser"
import secureLocalStorage from "react-secure-storage"

const initialState: ILogedInUser = {
    id: "",
    email: "",
    userName:"",
    accessToken: "",
    roles: [],
    isAdmin:false,
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
            state.roles = action.payload.roles!;
            state.isAdmin = action.payload.roles?.includes("Admin")?? false;

            secureLocalStorage.setItem("refreshToken", action.payload.refreshToken!)
        },
        logout: (state) => {
            state.id = "";
            state.email = "";
            state.userName = "";
            state.accessToken = "";
            state.refreshToken = "";
            state.isAdmin = false;
            state.roles = [];
            secureLocalStorage.removeItem("refreshToken")
        },
        toggelTheme: (state) => {
            state.userTheme = state.userTheme === "light" ? "dark" : "light";
            localStorage.setItem("theme", state.userTheme)
            if (state.userTheme === "dark") {
                document.documentElement.classList.add("dark");
            } else {
                document.documentElement.classList.remove("dark");
            }
        },
        initializeTheme: (state) => {

            const theme = localStorage.getItem("theme")
            state.userTheme = theme ||'light';
            if (state.userTheme === "dark") {
                document.documentElement.classList.add("dark");
            } else {
                document.documentElement.classList.remove("dark");
            }
        },
    }
});

export const { setLogedInUser, logout, toggelTheme, initializeTheme } = LogedInUserSlice.actions;

export default LogedInUserSlice;