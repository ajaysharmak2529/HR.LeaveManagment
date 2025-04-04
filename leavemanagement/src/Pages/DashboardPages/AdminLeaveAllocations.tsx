import { useState } from "react";
import Loader from "../../Components/Loader";
import { useGetAdminLeaveAllocarionsQuery } from "../../Services/LeaveAllocation.Service";
import Pagination from "../../Components/Pagination";

const AdminLeaveAllocations = () => {
    const [page, setPage] = useState({ page: 1, pageSize: 10 });
    const { isLoading, isError, error, data } = useGetAdminLeaveAllocarionsQuery(page);

    if (error) {
        console.error(error);
    }

    return (
        isLoading ? <Loader /> : isError ? <p className="text-red-500 bold">Unable to fetch data {(error as any).error}</p> :
            <div>
                <div className="min-h-screen rounded-2xl border border-gray-200 bg-white px-5 py-7 dark:border-gray-800 dark:bg-white/[0.03] xl:px-10 xl:py-12">
                    <h3 className="mb-4 font-semibold text-gray-800 text-theme-xl dark:text-white/90 sm:text-2xl">
                        Allocations
                    </h3>
                    <div className="overflow-hidden rounded-xl border border-gray-200 bg-white dark:border-white/[0.05] dark:bg-white/[0.03]">
                        <div className="max-w-full overflow-x-auto">
                            <div className="min-w-[720px]">
                                <table className="min-w-full">
                                    {/* Table Header */}
                                    <thead className="border-b border-gray-100 dark:border-white/[0.05]">
                                        <tr>
                                            <th className="px-2 py-3 font-medium text-gray-500 text-center text-theme-xs dark:text-gray-400">
                                                Leave Type
                                            </th>
                                            <th className="px-2 py-3 font-medium text-gray-500 text-center text-theme-xs dark:text-gray-400">
                                                EmployeeCount
                                            </th>
                                            <th className="px-2 py-3 font-medium text-gray-500 text-center text-theme-xs dark:text-gray-400">
                                                Period
                                            </th>
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
                                                data?.data?.items.map((leaveAllocation,i) => (
                                                    <tr key={i}>
                                                        <td className="px-4 py-3 text-gray-500 text-center text-theme-sm dark:text-gray-400">
                                                            {leaveAllocation.leaveType}
                                                        </td>
                                                        <td className="px-4 py-3 text-gray-500 text-center text-theme-sm dark:text-gray-400">
                                                            {leaveAllocation.employeeCount}
                                                        </td>
                                                        <td className="px-4 py-3 text-gray-500 text-center text-theme-sm dark:text-gray-400">
                                                            {leaveAllocation.year}
                                                        </td>
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

export default AdminLeaveAllocations;