export type LeaveRequestType = {
    id: number,
    leaveType: {
        id: number,
        name: string,
        defaultDays: number
    },
    dateRequested: string,
    approved: boolean,
    cancelled: boolean
    startDate: string,
    endDate: string,
    requestComments:string
}

export type CreateLeaveRequest = {
    startDate: string,
    endDate: string,
    leaveTypeId: number,
    dateRequested: string,
    requestComments: string
}
export type UpdateLeaveRequest =
    {
        id: number,
        startDate: string,
        endDate: string,
        leaveTypeId: number,
        requestComments: string,
        cancelled: boolean
    }
export type ChangeApprovelLeaveRequest =
    {
        id: number,
        approved: boolean
    }