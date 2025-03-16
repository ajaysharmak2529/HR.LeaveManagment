import Loader from "../../Components/Loader";
import { useGetLeaveRequestsQuery } from "../../Services/LeaveRequest.Service";
import { RootState } from "../../Redux/Store/Store"
import { useSelector } from "react-redux"
import Button from "../../Components/Button";
import Badge from "../../Components/Badge";
const LeaveRequests = () => {

    const { isLoading, isError, error, data } = useGetLeaveRequestsQuery("");
    const { isAdmin } = useSelector((state: RootState) => state.LogedInUser);

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
                                                        <td className="px-2 py-4 sm:px-6 text-center">
                                                            <div className="flex items-center gap-3">
                                                                <div>
                                                                    <span className="block font-medium text-gray-800 text-theme-sm dark:text-white/90">
                                                                        {request.leaveType.name}
                                                                    </span>
                                                                </div>
                                                            </div>
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
                                                            {new Date(request.leaveRequested).toLocaleDateString()}
                                                        </td>
                                                        {isAdmin && <td className="px-4 py-3 text-gray-500 text-center text-theme-sm dark:text-gray-400">
                                                            <div className="flex justify-center gap-4">
                                                                <Button type="button" onClick={() => { }} variant="primary">
                                                                    Approve
                                                                </Button>
                                                                <Button type="button" onClick={() => { }} variant="outline">
                                                                    Cancel
                                                                </Button>
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