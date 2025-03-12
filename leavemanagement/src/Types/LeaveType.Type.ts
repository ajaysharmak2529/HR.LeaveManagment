export type LeaveType = {
    id: number
    name: string,
    defaultDays: number

}

export type CreateLeaveTypeRequest = {
    name: string,
    defaultDays: number

}