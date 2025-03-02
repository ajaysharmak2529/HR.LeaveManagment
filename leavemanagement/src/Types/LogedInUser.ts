export interface ILogedInUser {
    id: number | null;
    email: String | null;
    userName: String | null;
    firstName: String | null;
    lastName: String | null;
    accessToken: String | null;
    refeshToken: String | null;
    userTheme: String | null;
}

export interface ILogedInUserSlice {
    id: number | null;
    email: String | null;
    userName: String | null;
    firstName: String | null;
    lastName: String | null;
    accessToken: String | null;
    refeshToken: String | null;
}