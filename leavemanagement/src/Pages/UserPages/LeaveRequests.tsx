import Loader from "../../Components/Loader";
import { useGetLeaveRequestsQuery, useChnageApprovalLeaveRequestMutation, useUpdateLeaveRequestMutation, useGetLeaveRequestQuery } from "../../Services/LeaveRequest.Service";
import { RootState } from "../../Redux/Store/Store"
import { useSelector } from "react-redux"
import Button from "../../Components/Button";
import Badge from "../../Components/Badge";
import { LeaveRequestType } from "../../Types/LeaveRequest.Type"

const LeaveRequests = () => {

    const { isLoading, isError, error, data } = useGetLeaveRequestsQuery("");
    const [changeApprovel] = useChnageApprovalLeaveRequestMutation();
    const [updateLeaveRequest] = useUpdateLeaveRequestMutation();

    const { isAdmin } = useSelector((state: RootState) => state.LogedInUser);

    const handelCancel = async (id: number) => {
        const request = data?.data.find(x => x.id == id) as LeaveRequestType;
        const { data: updateData, error } = await updateLeaveRequest({ cancelled: true, startDate: request.startDate, endDate: request.endDate, id: id, leaveTypeId: request.leaveType.id, requestComments: request.requestComments })

        if (updateData !== undefined) {
            console.log(updateData);
        }
        if (error !== undefined) {
            console.error(error);
        }

    }
    return (
        isLoading ? <Loader /> : isError ? <p className="text-red-500 bold">Unable to fetch data {(error as any).error}</p> :
            <div>
                <div className="min-h-screen rounded-2xl border border-gray-200 bg-white px-5 py-7 dark:border-gray-800 dark:bg-white/[0.03] xl:px-10 xl:py-12">
                    <h3 className="mb-4 font-semibold text-gray-800 text-theme-xl dark:text-white/90 sm:text-2xl">
                        Leave Requests
                    </h3>
                    <div className="overflow-hidden rounded-xl border border-gray-200 bg-white dark:border-white/[0.05] dark:bg-white/[0.03]">
                        <div className="max-w-full overflow-x-auto">
                            <div className="min-w-[720px]">
                                <table className="min-w-full divide-y divide-gray-200">
                                    {/* Table Header */}
                                    <thead className="border-b border-gray-100 dark:border-white/[0.05]">
                                        <tr>
                                            <th className="px-2 py-3 font-medium text-gray-500 text-center text-theme-xs dark:text-gray-400">
                                                Leave Type
                                            </th>
                                            <th className="px-2 py-3 font-medium text-gray-500 text-center text-theme-xs dark:text-gray-400">
                                                Default Days
                                            </th>
                                            <th className="px-2 py-3 font-medium text-gray-500 text-center text-theme-xs dark:text-gray-400">
                                                Is Approved
                                            </th>
                                            <th className="px-2 py-3 font-medium text-gray-500 text-center text-theme-xs dark:text-gray-400">
                                                Is Cancelled
                                            </th>
                                            <th className="px-2 py-3 font-medium text-gray-500 text-center text-theme-xs dark:text-gray-400">
                                                Leave Requested
                                            </th>
                                            {isAdmin && <th className="px-2 py-3 font-medium text-gray-500 text-center text-theme-xs dark:text-gray-400">
                                                Actions
                                            </th>}
                                        </tr>
                                    </thead>

                                    {/* Table Body */}
                                    <tbody className="divide-y divide-gray-100 dark:divide-white/[0.05]">
                                        {
                                            data?.data?.length == 0 ?
                                                <tr className="text-red-500 w-full">
                                                    <td className="px-2 py-4 sm:px-6 w-full text-center" colSpan={10}>No Data</td>
                                                </tr>
                                                :
                                                data?.data?.map((request) => (
                                                    <tr key={request.id}>
                                                        <td className="px-4 py-3 text-gray-500 text-center text-theme-sm dark:text-gray-400">
                                                            {request.leaveType.name}
                                                        </td>
                                                        <td className="px-4 py-3 text-gray-500 text-center text-theme-sm dark:text-gray-400">
                                                            {request.leaveType.defaultDays}
                                                        </td>
                                                        <td className="px-4 py-3 text-gray-500 text-center text-theme-sm dark:text-gray-400">
                                                            <Badge variant="solid" size="md" color={request.approved ? "success" : "warning"}>
                                                                {request.approved ? "Approved" : "Pending"}
                                                            </Badge>
                                                        </td>
                                                        <td className="px-4 py-3 text-gray-500 text-center text-theme-sm dark:text-gray-400">
                                                            <Badge variant="solid" size="md" color={request.cancelled ? "error" : "warning"}>
                                                                {request.cancelled ? "Cancelled" : "Not-Cancelled"}
                                                            </Badge>
                                                        </td>
                                                        <td className="px-4 py-3 text-gray-500 text-center text-theme-sm dark:text-gray-400">
                                                            {request.dateRequested}
                                                        </td>
                                                        {isAdmin && <td className="px-4 py-3 text-gray-500 text-center text-theme-sm dark:text-gray-400">
                                                            <div className="flex justify-center gap-4">
                                                                {(!request.approved && !request.cancelled) ? <>
                                                                    <Button type="button" size="sm" onClick={() => { changeApprovel({ id: request.id, approved: true }) }} variant="primary">
                                                                        Approve
                                                                    </Button>
                                                                    <Button type="button" size="sm" onClick={() => { handelCancel(request.id) }} variant="outline">
                                                                        Cancel
                                                                    </Button></>
                                                                    : <p>No Actions</p>}
                                                            </div>
                                                        </td>}
                                                    </tr>
                                                ))}
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    );
}

export default LeaveRequests;