export type LeaveRequestType = {
    id: number,
    leaveTypeId: {
        id: number,
        name: string,
        defaultDays: number
    },
    leaveRequested: string,
    approved: boolean
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