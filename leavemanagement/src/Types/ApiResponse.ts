export interface ApiResponse<T> {
    isSuccess: boolean;
    message: string;
    data: T;
    statusCode: number;
    errors: string[];
}

export type PageList<T> =
    {
        items: T;
        totalCount: number;
        page: number;
        pageSize: number;
        hasPreviousPage: boolean,
        hasNextPage: boolean
    }