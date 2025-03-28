import Loader from "../../Components/Loader";
import { useGetAllLeaveRequestsQuery, useChnageApprovalLeaveRequestMutation, useUpdateLeaveRequestMutation, useGetLeaveRequestQuery } from "../../Services/LeaveRequest.Service";
import { RootState } from "../../Redux/Store/Store"
import { useSelector } from "react-redux"
import Button from "../../Components/Button";
import Pagination from "../../Components/Pagination";
import Badge from "../../Components/Badge";
import { LeaveRequestType } from "../../Types/LeaveRequest.Type"
import { useState } from "react";
import { toast } from 'react-toastify';


const AdminLeaveRequests = () => {

    const [page, setPage] = useState({ page: 1, pageSize: 10 });
    const { isLoading, isError, error, data } = useGetAllLeaveRequestsQuery(page);
    const [changeApprovel] = useChnageApprovalLeaveRequestMutation();
    const [updateLeaveRequest] = useUpdateLeaveRequestMutation();

    const { isAdmin } = useSelector((state: RootState) => state.LogedInUser);

    const handelCancel = async (id: number) => {
        const request = data?.data.items.find(x => x.id == id) as LeaveRequestType;
        const { data: updateData, error } = await updateLeaveRequest({ cancelled: true, startDate: request.startDate, endDate: request.endDate, id: id, leaveTypeId: request.leaveType.id, requestComments: request.requestComments })

        if (updateData) {

            if (updateData.isSuccess) {
                toast.success(updateData.message);
            }
            else {

                for (let i = 0; i < updateData.errors.length; i++) {
                    toast.error(updateData.errors[i]);
                }
            }
        }
        if (error) {
            console.error(error);
        }
    }

    const handelChangeApproval = async (id: number) => {
        const { data, error } = await changeApprovel({ id, approved: true })

        if (data) {
            if (data.isSuccess) {
                toast.success(data.message);
            }
            else {
                for (let i = 0; i < data.errors.length; i++) {
                    toast.error(data.errors[i]);
                }
            }
        }
        else if (error) {
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
                                                Status
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
                                            data?.data?.items.length == 0 ?
                                                <tr className="text-red-500 w-full">
                                                    <td className="px-2 py-4 sm:px-6 w-full text-center" colSpan={10}>No Data</td>
                                                </tr>
                                                :
                                                data?.data?.items.map((request) => (
                                                    <tr key={request.id}>
                                                        <td className="px-4 py-3 text-gray-500 text-center text-theme-sm dark:text-gray-400">
                                                            {request.leaveType.name}
                                                        </td>
                                                        <td className="px-4 py-3 text-gray-500 text-center text-theme-sm dark:text-gray-400">
                                                            {request.leaveType.defaultDays}
                                                        </td>
                                                        <td className="px-4 py-3 text-gray-500 text-center text-theme-sm dark:text-gray-400">
                                                            <Badge variant="solid" size="md" color={request.approved ? "success" : request.cancelled ? "error" : "warning"}>
                                                                {request.approved ? "Approved" : request.cancelled ? "Cancelled" : "Pending"}
                                                            </Badge>
                                                        </td>
                                                        <td className="px-4 py-3 text-gray-500 text-center text-theme-sm dark:text-gray-400">
                                                            {request.dateRequested}
                                                        </td>
                                                        {isAdmin && <td className="px-4 py-3 text-gray-500 text-center text-theme-sm dark:text-gray-400">
                                                            <div className="flex justify-center gap-4">
                                                                {(!request.approved && !request.cancelled) ? <>
                                                                    <Button type="button" size="sm" onClick={async () => { await handelChangeApproval(request.id) }} variant="primary">
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
                    <div className="flex justify-end mt-4">
                        <Pagination
                            hasNextPage={data?.data?.hasNextPage ?? false}
                            hasPreviousPage={data?.data?.hasPreviousPage ?? false}
                            activePage={data?.data?.page ?? 1}
                            onNext={() => { setPage({ ...page, page: page.page + 1 }) }}
                            onPrevious={() => { setPage({ ...page, page: page.page - 1 }) }}
                            pageSize={data?.data?.pageSize ?? 1}
                            totalCount={data?.data?.totalCount ?? 1}
                            onSetPage={(p) => { setPage({ ...page, page: p }) }}
                        />
                    </div>
                </div>
            </div>
    );
}

export default AdminLeaveRequests;