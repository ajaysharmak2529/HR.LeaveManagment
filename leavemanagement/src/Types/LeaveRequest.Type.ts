export type LeaveRequestType = {
    id: number,
    leaveType: {
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
    leaveTypeId: 0,
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