import { FormEvent, useState } from "react";
import { useGetLeaveTypesQuery } from "../../Services/LeaveType.Service"
import { useAddLeaveRequestMutation } from "../../Services/LeaveRequest.Service"
import { useGetEmployeeLeaveAllocarionsQuery } from "../../Services/LeaveAllocation.Service"
import { useEmployeeReportQuery } from "../../Services/Reports.Service"
import { CreateLeaveRequest } from "../../Types/LeaveRequest.Type";
import { TbReportAnalytics } from "react-icons/tb";
import Input from "../../Components/InputField";
import TextArea from "../../Components/TextArea";
import Button from "../../Components/Button";
import Label from "../../Components/Label";
import { format, addDays } from 'date-fns';
import Select, { Option } from "../../Components/Select";
import { toast } from 'react-toastify';


const Index = () => {

    const [page, setPage] = useState({ page: 1, pageSize: 1000 });
    const [alocationPage, setAlocationPage] = useState({ page: 1, pageSize: 10 });

    const { data } = useGetLeaveTypesQuery(page);

    const [leaveRequest, setLeaveRequest] = useState<CreateLeaveRequest>({ startDate: format(new Date(), "yyyy-MM-dd"), endDate: format(addDays(new Date(), 1), "yyyy-MM-dd"), dateRequested: format(new Date(), "dd/MM/yyyy"), leaveTypeId: 2, requestComments: "Test-1" })
    const [addLeaveRequest] = useAddLeaveRequestMutation();
    const { data: employeeReport } = useEmployeeReportQuery("");
    const { data: employeeAloocations } = useGetEmployeeLeaveAllocarionsQuery(alocationPage);


    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        leaveRequest.startDate = format(leaveRequest.startDate, "dd/MM/yyyy");
        leaveRequest.endDate = format(leaveRequest.endDate, "dd/MM/yyyy");

        const { data: response, error: reponseError } = await addLeaveRequest(leaveRequest);

        if (response) {
            if (response?.isSuccess) {

                toast.success(response.message);

            } else if (!response?.isSuccess) {

                for (let i = 0; i < response.errors.length; i++) {
                    toast.error(response.errors[i]);
                }
            }
        }

        if (reponseError) {
            console.error((reponseError as any).error);
        }
    }
    const options: Option[] = data?.data.items.map((type) => ({
        value: type.id,
        label: type.name,
    })) ?? [];
    console.log(employeeAloocations?.data)
    return (
        <>
            <div className="bg-white p-6 rounded-lg shadow-md dark:border-gray-800 dark:bg-white/[0.03] dark:text-white">
                <div className="flex items-center mb-6">
                    <TbReportAnalytics fontSize="30" color="red" />
                    <h2 className="text-2xl font-bold">Leave Report</h2>
                </div>
                <div className="grid grid-cols-1 md:grid-cols-4 gap-4 mb-8">
                    <div className="bg-blue-50 p-4 rounded-lg">
                        <p className="text-sm text-blue-600">Total Requests</p>
                        <p className="text-2xl font-bold">{employeeReport?.data.totalRequests ?? 0}</p>
                    </div>
                    <div className="bg-green-50 p-4 rounded-lg">
                        <p className="text-sm text-green-600">Approved</p>
                        <p className="text-2xl font-bold">{employeeReport?.data.approved ?? 0}</p>
                    </div>
                    <div className="bg-red-50 p-4 rounded-lg">
                        <p className="text-sm text-red-600">Rejected</p>
                        <p className="text-2xl font-bold">{employeeReport?.data.rejected ?? 0}</p>
                    </div>
                    <div className="bg-yellow-50 p-4 rounded-lg">
                        <p className="text-sm text-yellow-600">Pending</p>
                        <p className="text-2xl font-bold">{employeeReport?.data.pending ?? 0}</p>
                    </div>
                </div>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
                    <form onSubmit={handleSubmit} className="space-y-4 bg-white p-6 rounded-lg shadow-md">
                        <h2 className="text-2xl font-bold mb-4">Request Leave</h2>
                        <div>
                            <Label>Leave Type</Label>
                            <Select
                                options={options}
                                defaultValue={leaveRequest.leaveTypeId.toString()}
                                onChange={(e) => setLeaveRequest({ ...leaveRequest, leaveTypeId: parseInt(e) })}
                                isRequired={true}
                            />
                        </div>
                        <div>
                            <Label>Start Date</Label>
                            <Input
                                type="date"
                                value={leaveRequest.startDate}
                                onChange={(e) => setLeaveRequest({ ...leaveRequest, startDate: e.target.value })}
                            />
                        </div>
                        <div>
                            <Label>End Date</Label>
                            <Input
                                type="date"
                                value={leaveRequest.endDate}
                                onChange={(e) => setLeaveRequest({ ...leaveRequest, endDate: e.target.value })}
                            />
                        </div>
                        <div>
                            <Label>Comments</Label>
                            <TextArea
                                value={leaveRequest.requestComments}
                                onChange={(value) => setLeaveRequest({ ...leaveRequest, requestComments: value })}
                                hint="Optional"
                            />
                        </div>
                        <Button
                            type="submit"
                            className="flex items-center justify-center w-full px-4 py-2 text-white bg-green-600 rounded-md hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-green-500 focus:ring-offset-2"
                        >
                            Submit Request
                        </Button>
                    </form>
                    <div>
                        <h3 className="text-xl font-semibold mb-4">Alloted Leaves </h3>
                        <div className="space-y-4">
                            {employeeAloocations?.data.items.map(allocation => {
                                return (
                                    <div key={allocation.id} className="bg-gray-50 p-4 rounded-lg">
                                        <h4 className="font-medium font-bold text-gray-700">{allocation.leaveType.name}</h4>
                                        <div className="grid grid-cols-2 gap-4 mt-2">
                                            <div>
                                                <p className="text-sm text-gray-500">Number Of Days</p>
                                                <p className="text-lg font-semibold">{allocation.numberOfDays}</p>
                                            </div>
                                            <div>
                                                <p className="text-sm text-gray-500">Period</p>
                                                <p className="text-lg font-semibold">{allocation.period}</p>
                                            </div>
                                        </div>
                                    </div>
                                );
                            })}
                        </div>
                    </div>
                </div>


            </div>
        </>
    )
}
export default Index;