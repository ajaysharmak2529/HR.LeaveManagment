import { useState, useEffect } from 'react';
import { Outlet } from 'react-router-dom';
import Sidebar from '../../Components/DashboardSidebar';
import { useSelector } from 'react-redux';
import { RootState } from "../../Redux/Store/Store";
import DashboardHeader from '../../Components/DashboardHeader';
import Backdrop from '../../Components/Backdrop';

const AdminLayout = () => {
    const [isSidebarOpen, setIsSidebarOpen] = useState(true);
    const { isExpanded, isHovered, isMobileOpen } = useSelector((state: RootState) => state.Sidebar);

    useEffect(() => {
        const handleResize = () => {
            if (window.innerWidth >= 1024 && isSidebarOpen) {
                setIsSidebarOpen(false);
            }
        };

        window.addEventListener("resize", handleResize);
        return () => window.removeEventListener("resize", handleResize);
    }, [isSidebarOpen]);

    return (
        <div className="min-h-screen xl:flex">
            <div>
                <Sidebar />
                <Backdrop />
            </div>
            <div
                className={`flex-1 transition-all duration-300 ease-in-out ${isExpanded || isHovered ? "lg:ml-[290px]" : "lg:ml-[90px]"
                    } ${isMobileOpen ? "ml-0" : ""}`}
            >
                <DashboardHeader />
                <div className="p-4 mx-auto max-w-(--breakpoint-2xl) md:p-6">
                    <Outlet />
                </div>
            </div>
        </div>
    );
};

export default AdminLayout;