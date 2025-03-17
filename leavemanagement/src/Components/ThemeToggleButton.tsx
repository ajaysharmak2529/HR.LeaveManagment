import { useDispatch } from "react-redux";
import { toggelTheme, initializeTheme } from "../Redux/Slices/LogedInUserSlice";
import { useEffect } from "react";
import { IoMoonOutline } from "react-icons/io5";
import { MdOutlineWbSunny } from "react-icons/md";

export const ThemeToggleButton: React.FC = () => {
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(initializeTheme());
    }, [])

    return (
        <button type='button'
            onClick={() => { dispatch(toggelTheme()); }}
            className="relative flex items-center justify-center text-gray-500 transition-colors bg-white border border-gray-200 rounded-full hover:text-dark-900 h-11 w-11 hover:bg-gray-100 hover:text-gray-700 dark:border-gray-800 dark:bg-gray-900 dark:text-gray-400 dark:hover:bg-gray-800 dark:hover:text-white"
        >
            <IoMoonOutline className="dark:hidden" fontSize="20" />
            <MdOutlineWbSunny className="hidden dark:block" fontSize="20" />
        </button>
    );
};
