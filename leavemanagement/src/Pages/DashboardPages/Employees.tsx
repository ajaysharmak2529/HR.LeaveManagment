import Loader from "../../Components/Loader";
import { useGetEmployeesQuery } from "../../Services/Employee.Service";
import Button from "../../Components/Button";
import Input from "../../Components/InputField";
import Label from "../../Components/Label";
import { openModal, closeModal } from "../../Redux/Slices/Modal.Slice"
import { RootState } from "../../Redux/Store/Store"
import { useSelector, useDispatch } from "react-redux"
import { Modal } from "../../Components/Modal";
import { useState } from "react";

const Employees = () => {
    const [page, setPage] = useState({ page: 1, pageSize: 10 });
    const { isLoading, isError, error, data } = useGetEmployeesQuery(page);
    const { isOpen } = useSelector((state: RootState) => state.modal);
    const dispatch = useDispatch();

    return (
        isLoading ? <Loader /> : isError ? <p className="text-red-500 bold">Unable to fetch data {(error as any).error}</p> :
            < div >
                <div className="min-h-screen rounded-2xl border border-gray-200 bg-white px-2 py-7 dark:border-gray-800 dark:bg-white/[0.03] xl:px-10 xl:py-12">
                    <h3 className="mb-4 font-semibold text-gray-800 text-theme-xl dark:text-white/90 sm:text-2xl">
                        Employees
                    </h3>
                    <div className="flex justify-end mb-5">
                        <Button type="button" variant="outline" size="sm" onClick={() => { dispatch(openModal()) }}>Create</Button>
                    </div>
                    <div className="overflow-hidden rounded-xl border border-gray-200 bg-white dark:border-white/[0.05] dark:bg-white/[0.03]">
                        <div className="max-w-full overflow-x-auto">
                            <div className="min-w-[720px]">
                                <table className="min-w-full">
                                    {/* Table Header */}
                                    <thead className="border-b border-gray-100 dark:border-white/[0.05]">
                                        <tr>
                                            <th className="px-2 py-3 font-medium text-gray-500 text-center text-theme-xs dark:text-gray-400">
                                                First Name
                                            </th>
                                            <th className="px-2 py-3 font-medium text-gray-500 text-center text-theme-xs dark:text-gray-400">
                                                Last Name
                                            </th>
                                            <th className="px-2 py-3 font-medium text-gray-500 text-center text-theme-xs dark:text-gray-400">
                                                Email
                                            </th>
                                        </tr>
                                    </thead>

                                    {/* Table Body */}
                                    <tbody className="divide-y divide-gray-100 dark:divide-white/[0.05]">
                                        {
                                            data?.data?.items?.length == 0 ?
                                                <tr className="text-red-500 w-full">
                                                    <td className="px-2 py-4 sm:px-6 w-full text-center" colSpan={10}>No Data</td>
                                                </tr>
                                                :
                                                data?.data?.items?.map((employee) => (
                                                    <tr key={employee.id}>
                                                        <td className="px-2 py-4 sm:px-6 text-center">
                                                            <span className="block font-medium text-gray-800 text-center text-theme-sm dark:text-white/90">
                                                                {employee.firstName}
                                                            </span>
                                                        </td>
                                                        <td className="px-4 py-3 text-gray-500 text-center text-theme-sm dark:text-gray-400">
                                                            {employee.lastName}
                                                        </td>
                                                        <td className="px-4 py-3 text-gray-500 text-center text-theme-sm dark:text-gray-400">
                                                            {employee.email}
                                                        </td>
                                                    </tr>
                                                ))}
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <Modal isOpen={isOpen} onClose={() => { dispatch(closeModal()) }} isFullscreen={false} className="max-w-[700px] m-4">
                    <div className="no-scrollbar relative w-full max-w-[700px] overflow-y-auto rounded-3xl bg-white p-4 dark:bg-gray-900 lg:p-11">
                        <div className="px-2 pr-14">
                            <h4 className="mb-2 text-2xl font-semibold text-gray-800 dark:text-white/90">
                                Edit Personal Information
                            </h4>
                            <p className="mb-6 text-sm text-gray-500 dark:text-gray-400 lg:mb-7">
                                Update your details to keep your profile up-to-date.
                            </p>
                        </div>
                        <form className="flex flex-col">
                            <div className="custom-scrollbar h-[450px] overflow-y-auto px-2 pb-3">
                                <div className="mt-7">
                                    <h5 className="mb-5 text-lg font-medium text-gray-800 dark:text-white/90 lg:mb-6">
                                        Personal Information
                                    </h5>
                                    <div className="grid grid-cols-1 gap-x-6 gap-y-5 lg:grid-cols-2">
                                        <div className="col-span-2 lg:col-span-1">
                                            <Label>First Name</Label>
                                            <Input type="text" value="Musharof" />
                                        </div>

                                        <div className="col-span-2 lg:col-span-1">
                                            <Label>Last Name</Label>
                                            <Input type="text" value="Chowdhury" />
                                        </div>

                                        <div className="col-span-2 lg:col-span-1">
                                            <Label>Email Address</Label>
                                            <Input type="text" value="randomuser@pimjo.com" />
                                        </div>

                                        <div className="col-span-2 lg:col-span-1">
                                            <Label>Phone</Label>
                                            <Input type="text" value="+09 363 398 46" />
                                        </div>

                                        <div className="col-span-2">
                                            <Label>Bio</Label>
                                            <Input type="text" value="Team Manager" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div className="flex items-center gap-3 px-2 mt-6 lg:justify-end">
                                <Button type="button" size="sm" variant="outline" onClick={() => { dispatch(closeModal()) }}>
                                    Close
                                </Button>
                                <Button type="button" size="sm" onClick={() => { }}>
                                    Save Changes
                                </Button>
                            </div>
                        </form>
                    </div>
                </Modal>
            </div>
    );
}

export default Employees;