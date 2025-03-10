export type LeaveAloocationType = {
    id: number,
    numberOfDays: number,
    leaveType: {
        id: number,
        name: string,
        defaultDays: number
    },
    leaveTypeId: number,
    period: number
}

export type CreateLeaveAllocation = {
    numberOfDays: number,
    leaveTypeId: number,
    period: number
}