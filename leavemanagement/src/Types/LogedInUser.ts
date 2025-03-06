export interface ILogedInUser {
    id: string | null;
    email: string | null;
    userName: string | null;
    accessToken: string | null;
    refreshToken: string;
    userTheme: string | null;
}

export interface ILogedInUserSlice {
    id: string | null;
    email: string | null;
    userTheme: string | null;
    userName: string | null;
    accessToken: string | null;
    refreshToken: string | null;
}

export interface ILogin{
    email: string;
    password: string;
}
export interface ISignUp {
    firstName: string;
    lastName: string;
    userName: string;
    email: string;
    password: string;
    confirmPassword: string;
}