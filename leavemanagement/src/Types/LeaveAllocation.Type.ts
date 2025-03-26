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
export type AdminLeaveAloocationType = {
    leaveType: string,
    year: number,
    employeeCount: number
}

export type CreateLeaveAllocation = {
    leaveTypeId: number
}