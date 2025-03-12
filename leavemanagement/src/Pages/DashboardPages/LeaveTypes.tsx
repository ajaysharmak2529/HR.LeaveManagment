import Loader from "../../Components/Loader";
import { useGetLeaveTypesQuery } from "../../Services/LeaveType.Service";
import Button from "../../Components/Button";
import Input from "../../Components/InputField";
import Label from "../../Components/Label";
import { openModal, closeModal } from "../../Redux/Slices/Modal.Slice"
import { RootState } from "../../Redux/Store/Store"
import { useSelector, useDispatch } from "react-redux"
import { Modal } from "../../Components/Modal";
import { FormEvent, useState, ChangeEvent } from "react";
import { useAddLeaveTypeMutation, useUpdateLeaveTypeMutation, useDeleteLeaveTypeMutation, useGetLeaveTypeMutation } from "../../Services/LeaveType.Service";
import { CreateLeaveTypeRequest, LeaveType, } from "../../Types/LeaveType.Type";

const LeaveTypes = () => {

    const { isLoading, isError, error, data } = useGetLeaveTypesQuery("");
    const [addLeaveType, { isLoading: addIsloading, isError: addIsError, error: addError }] = useAddLeaveTypeMutation();
    const [deleteLeaveType, { isLoading: deleteloading, isError: deleteIsError, error: deleteError }] = useDeleteLeaveTypeMutation();
    const [updateLeaveType, { isLoading: updateloading, isError: updateIsError, error: updateError }] = useUpdateLeaveTypeMutation();
    const [getLeaveType, { isLoading: getLeaveTypeLoading, isError: getLeaveTypeIsError, error: getLeaveTypeError }] = useGetLeaveTypeMutation();

    const { isOpen } = useSelector((state: RootState) => state.modal);
    const [isEditMode, seteditMode] = useState(false);
    const dispatch = useDispatch();
    const [leaveType, setLeaveType] = useState<LeaveType>({ id: 0, defaultDays: 0, name: "" });


    const handelCloseModal = () => {
        dispatch(closeModal());
        seteditMode(false);
    }
    const handelDelete = async (id: number) => {
        var { data } = await deleteLeaveType(id);
        if (data?.isSuccess) {

        } else {

        }
    };
    const handelEdit = async (id: number) => {
        seteditMode(true);
        dispatch(openModal());
        const { data } = await getLeaveType(id);

        if (data?.isSuccess) {
            setLeaveType(data.data);
        }
        if (getLeaveTypeIsError) {
            console.log(getLeaveTypeError);
        }

    }

    const handelFormSubmition = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        if (!isEditMode) {
            const { data } = await addLeaveType({ name: leaveType.name, defaultDays: leaveType.defaultDays });

            if (data?.isSuccess) {
                handelCloseModal();

            } else {
                console.log(addError);
            }
            

        } else {
            const { data } = await updateLeaveType(leaveType);

            if (data?.isSuccess) {
                handelCloseModal();

            } else {
                console.log(data);
            }
            if (updateIsError) {
                console.log(updateError);
            }
        }
        seteditMode(false);
    }



    return (
        isLoading ? <Loader /> : isError ? <p className="text-red-500 bold">Unable to fetch data {(error as any).error}</p> :
            <div>
                <div className="min-h-screen rounded-2xl border border-gray-200 bg-white px-5 py-7 dark:border-gray-800 dark:bg-white/[0.03] xl:px-10 xl:py-12">
                    <h3 className="mb-4 font-semibold text-gray-800 text-theme-xl dark:text-white/90 sm:text-2xl">
                        Leave Types
                    </h3>
                    <div className="flex justify-end mb-5">
                        <Button variant="outline" size="sm" onClick={() => { dispatch(openModal()) }}>Create</Button>
                    </div>
                    <div className="w-full overflow-hidden rounded-xl border border-gray-200 bg-white dark:border-white/[0.05] dark:bg-white/[0.03]">
                        <div className="max-w-full overflow-x-auto">
                            <div className="min-w-[720px]">
                                <table className="min-w-full">
                                    {/* Table Header */}
                                    <thead className="border-b border-gray-100 dark:border-white/[0.05]">
                                        <tr>
                                            <th className="px-2 py-3 font-medium text-gray-500 text-center text-theme-xs dark:text-gray-400">
                                                Name
                                            </th>
                                            <th className="px-2 py-3 font-medium text-gray-500 text-center text-theme-xs dark:text-gray-400">
                                                Default Days
                                            </th>
                                            <th className="px-2 py-3 font-medium text-gray-500 text-center text-theme-xs dark:text-gray-400">
                                                Actions
                                            </th>
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
                                                data?.data?.map((leaveType) => (
                                                    <tr key={leaveType.id}>
                                                        <td className="px-2 py-4 sm:px-6 text-center">
                                                            <span className="block font-medium text-gray-800 text-theme-sm dark:text-white/90">
                                                                {leaveType.name}
                                                            </span>

                                                        </td>
                                                        <td className="px-4 py-3 text-gray-500 text-center text-theme-sm dark:text-gray-400">
                                                            {leaveType.defaultDays}
                                                        </td>
                                                        <td className="py-3 px-4 text-center">
                                                            <div className="flex item-center justify-center">
                                                                <button className="w-4 mr-2 transform text-blue-500 hover:scale-140" onClick={async () => { await handelEdit(leaveType.id) }}>
                                                                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                                                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15.232 5.232l3.536 3.536m-2.036-5.036a2.5 2.5 0 113.536 3.536L6.5 21.036H3v-3.572L16.732 3.732z" />
                                                                    </svg>
                                                                </button>
                                                                <button className="w-4 mr-2 transform text-red-500 hover:scale-140" onClick={async () => { await handelDelete(leaveType.id) }}>
                                                                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                                                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" />
                                                                    </svg>
                                                                </button>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                ))}
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <Modal isOpen={isOpen} onClose={handelCloseModal} isFullscreen={false} className="max-w-[700px] m-4">
                    <div className="no-scrollbar relative w-full max-w-[700px] overflow-y-auto rounded-3xl bg-white p-4 dark:bg-gray-900 lg:p-11">
                        <div className="px-2 pr-14">
                            <h4 className="mb-2 text-2xl font-semibold text-gray-800 dark:text-white/90">
                                Edit Personal Information
                            </h4>
                            <p className="mb-6 text-sm text-gray-500 dark:text-gray-400 lg:mb-7">
                                Update your details to keep your profile up-to-date.
                            </p>
                        </div>
                        <form className="flex flex-col" onSubmit={handelFormSubmition}>
                            <div className="custom-scrollbar h-[450px] overflow-y-auto px-2 pb-3">
                                <div className="mt-7">
                                    <h5 className="mb-5 text-lg font-medium text-gray-800 dark:text-white/90 lg:mb-6">
                                        Personal Information
                                    </h5>
                                    <div className="grid grid-cols-1 gap-x-6 gap-y-5 lg:grid-cols-2">
                                        <div className="col-span-2 lg:col-span-1">
                                            <Label>Default Days</Label>
                                            <Input type="number" name="defaultDays" value={leaveType.defaultDays} onChange={(e: ChangeEvent<HTMLInputElement>) => { setLeaveType({ ...leaveType, defaultDays: parseInt(e.target.value) }) }} />
                                        </div>

                                        <div className="col-span-2 lg:col-span-1">
                                            <Label>Name</Label>
                                            <Input type="text" name="name" value={leaveType.name} onChange={(e: ChangeEvent<HTMLInputElement>) => { setLeaveType({ ...leaveType, name: e.target.value }) }} />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div className="flex items-center gap-3 px-2 mt-6 lg:justify-end">
                                <Button size="sm" variant="outline" onClick={handelCloseModal}>
                                    Close
                                </Button>
                                <Button size="sm" disabled={addIsloading}>
                                    {addIsloading ? "Processing ..." : "Save Changes"}
                                </Button>
                            </div>
                        </form>
                    </div>
                </Modal>
            </div>
    );
}

export default LeaveTypes;