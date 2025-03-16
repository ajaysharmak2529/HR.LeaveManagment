export interface ILogedInUser {
    id: string | null;
    email: string | null;
    userName: string | null;
    accessToken: string | null;
    refreshToken: string;
    roles: string[] | null;
    isAdmin: boolean;
    userTheme: string | null;
}

export interface ILogedInUserSlice {
    id: string | null;
    email: string | null;
    userTheme: string | null;
    userName: string | null;
    accessToken: string | null;
    refreshToken: string | null;
    roles: string[] | null;
    isAdmin: boolean;
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