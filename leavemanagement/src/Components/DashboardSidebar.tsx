import { Link, useLocation } from "react-router";
import { useCallback, useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { RootState } from "../Redux/Store/Store";
import { setIsHovered, setIsMobile, setIsMobileOpen } from "../Redux/Slices/Sidebar.Slice";
import { BsPersonCircle } from "react-icons/bs";
import { BsJournalCheck } from "react-icons/bs";
import { BsJournalText } from "react-icons/bs";
import { LuFileType } from "react-icons/lu";

const Sidebar = () => {
    const location = useLocation();
    const { isExpanded, isHovered, isMobileOpen, isMobile } = useSelector((state: RootState) => state.Sidebar);
    const dispatch = useDispatch();

    const isActive = useCallback(
        (path: string) => location.pathname === path,
        [location.pathname]
    );

    useEffect(() => {
        const handleResize = () => {
            const mobile = window.innerWidth < 768;
            dispatch(setIsMobile(mobile));
            if (!mobile) {
                dispatch(setIsMobileOpen(false));
            }
        };

        handleResize();
        window.addEventListener("resize", handleResize);

        return () => {
            window.removeEventListener("resize", handleResize);
        };
    }, []);

    return (
        <aside
            className={`fixed mt-16 flex flex-col lg:mt-0 top-0 px-5 left-0 bg-white dark:bg-gray-900 dark:border-gray-800 text-gray-900 h-screen transition-all duration-300 ease-in-out z-50 border-r border-gray-200 
        ${isExpanded || isMobileOpen
                    ? "w-[290px]"
                    : isHovered
                        ? "w-[290px]"
                        : "w-[90px]"
                }
        ${isMobileOpen ? "translate-x-0" : "-translate-x-full"}
        lg:translate-x-0`}
            onMouseEnter={() => !isExpanded && dispatch(setIsHovered(true))}
            onMouseLeave={() => dispatch(setIsHovered(false))}
        >
            <div
                className={`py-8 flex ${!isExpanded && !isHovered ? "lg:justify-center" : "justify-start"
                    }`}
            >
                <Link to="/dashboard">
                    {isExpanded || isHovered || isMobileOpen ? (
                        <>
                            <img
                                className="dark:hidden"
                                src="/images/logo/logo.svg"
                                alt="Logo"
                                width={150}
                                height={40}
                            />
                            <img
                                className="hidden dark:block"
                                src="/images/logo/logo-dark.svg"
                                alt="Logo"
                                width={150}
                                height={40}
                            />
                        </>
                    ) : (
                        <img
                            src="/images/logo/logo-icon.svg"
                            alt="Logo"
                            width={32}
                            height={32}
                        />
                    )}
                </Link>
            </div>
            <div className="flex flex-col overflow-y-auto duration-300 ease-linear no-scrollbar">
                <nav className="mb-6">
                    <div className="flex flex-col gap-4">
                        <div>
                            <h2
                                className={`mb-4 text-xs uppercase flex leading-[20px] text-gray-400 ${!isExpanded && !isHovered
                                    ? "lg:justify-center"
                                    : "justify-start"
                                    }`}
                            >
                                Menu
                            </h2>
                            <ul className="flex flex-col gap-4">                                
                                <li key={"2"}>
                                    <Link
                                        to={"/dashboard/leave-types"}
                                        className={`menu-item group ${isActive("/dashboard/leave-types") ? "menu-item-active" : "menu-item-inactive"
                                            }`}
                                    >
                                        <span
                                            className={`menu-item-icon-size ${isActive("/dashboard/leave-types")
                                                ? "menu-item-icon-active"
                                                : "menu-item-icon-inactive"
                                                }`}
                                        >
                                            <LuFileType />
                                        </span>
                                        {(isExpanded || isHovered || isMobileOpen) && (
                                            <span className="menu-item-text">Leave Types</span>
                                        )}
                                    </Link>
                                </li>
                                <li key={"3"}>
                                    <Link
                                        to={"/dashboard/leave-requests"}
                                        className={`menu-item group ${isActive("/dashboard/leave-requests") ? "menu-item-active" : "menu-item-inactive"
                                            }`}
                                    >
                                        <span
                                            className={`menu-item-icon-size ${isActive("/dashboard/leave-requests")
                                                ? "menu-item-icon-active"
                                                : "menu-item-icon-inactive"
                                                }`}
                                        >
                                            <BsJournalText />
                                        </span>
                                        {(isExpanded || isHovered || isMobileOpen) && (
                                            <span className="menu-item-text">Leave Requests</span>
                                        )}
                                    </Link>
                                </li>
                                <li key={"4"}>
                                    <Link
                                        to={"/dashboard/leave-allocations"}
                                        className={`menu-item group ${isActive("/dashboard/leave-allocations") ? "menu-item-active" : "menu-item-inactive"
                                            }`}
                                    >
                                        <span
                                            className={`menu-item-icon-size ${isActive("/dashboard/leave-allocations")
                                                ? "menu-item-icon-active"
                                                : "menu-item-icon-inactive"
                                                }`}
                                        >
                                            <BsJournalCheck />
                                        </span>
                                        {(isExpanded || isHovered || isMobileOpen) && (
                                            <span className="menu-item-text">Leave Allocations</span>
                                        )}
                                    </Link>
                                </li>
                                <li key={"5"}>
                                    <Link
                                        to={"/dashboard/leave-employees"}
                                        className={`menu-item group ${isActive("/dashboard/leave-employees") ? "menu-item-active" : "menu-item-inactive"
                                            }`}
                                    >
                                        <span
                                            className={`menu-item-icon-size ${isActive("/dashboard/leave-employees")
                                                ? "menu-item-icon-active"
                                                : "menu-item-icon-inactive"
                                                }`}
                                        >
                                            <BsPersonCircle />
                                        </span>
                                        {(isExpanded || isHovered || isMobileOpen) && (
                                            <span className="menu-item-text">Leave Employees</span>
                                        )}
                                    </Link>
                                </li>
                            </ul>
                        </div>
                    </div>
                </nav>
            </div>
        </aside>
    );

}
export default Sidebar;
