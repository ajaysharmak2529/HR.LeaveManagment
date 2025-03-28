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
            </div>
        </>
    );
}

export default Dashboard;