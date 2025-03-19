import { TbReportAnalytics } from "react-icons/tb";
import { useAdminReportQuery } from "../../Services/Reports.Service"

const Dashboard = () => {

    const { data: adminReport } = useAdminReportQuery("");

    return (
        <>
            <div className="bg-white p-6 rounded-lg shadow-md dark:border-gray-800 dark:bg-white/[0.03] dark:text-white">
                <div className="flex items-center mb-6">
                    <TbReportAnalytics fontSize="30" color="red" />
                    <h2 className="text-2xl font-bold">Leave Report</h2>
                </div>

                <div className="grid grid-cols-1 md:grid-cols-4 gap-4 mb-8 text-gray-800">
                    <div className="bg-blue-50 p-4 rounded-lg">
                        <p className="text-sm text-blue-600">Total Requests</p>
                        <p className="text-2xl font-bold ">{adminReport?.data.totalRequests}</p>
                    </div>
                    <div className="bg-green-50 p-4 rounded-lg">
                        <p className="text-sm text-green-600">Approved</p>
                        <p className="text-2xl font-bold">{adminReport?.data.approved}</p>
                    </div>
                    <div className="bg-red-50 p-4 rounded-lg">
                        <p className="text-sm text-red-600">Rejected</p>
                        <p className="text-2xl font-bold">{adminReport?.data.rejected}</p>
                    </div>
                    <div className="bg-yellow-50 p-4 rounded-lg">
                        <p className="text-sm text-yellow-600">Pending</p>
                        <p className="text-2xl font-bold">{adminReport?.data.pending}</p>
                    </div>
                </div>

                <h3 className="text-xl font-semibold mb-4">Leave Types Breakdown</h3>
                <div className="space-y-4">
                    {/*{leaveTypes.map(type => {*/}
                    {/*    const typeStats = stats.byType[type.id];*/}
                    {/*    return (*/}
                    {/*        <div key={type.id} className="bg-gray-50 p-4 rounded-lg">*/}
                    {/*            <h4 className="font-medium text-gray-700">{type.name}</h4>*/}
                    {/*            <div className="grid grid-cols-2 gap-4 mt-2">*/}
                    {/*                <div>*/}
                    {/*                    <p className="text-sm text-gray-500">Total Requests</p>*/}
                    {/*                    <p className="text-lg font-semibold">{typeStats.total}</p>*/}
                    {/*                </div>*/}
                    {/*                <div>*/}
                    {/*                    <p className="text-sm text-gray-500">Approved</p>*/}
                    {/*                    <p className="text-lg font-semibold">{typeStats.approved}</p>*/}
                    {/*                </div>*/}
                    {/*            </div>*/}
                    {/*        </div>*/}
                    {/*    );*/}
                    {/*})}*/}
                </div>
            </div>
        </>
    );
}

export default Dashboard;