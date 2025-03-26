import { BrowserRouter as Router, Route, Routes } from "react-router-dom"
import ProtectedRoute from "./ProtectedRoute";
import SignUp from "../Pages/SignUp";
import UserLayout from "../Pages/Shared/UserLayout";
import AdminLayout from "../Pages/Shared/AdminLayout";
import AdminLeaveRequests from "../Pages/DashboardPages/AdminLeaveRequests";
import Index from "../Pages/UserPages/Index";
import EmployeeLeaveRequests from "../Pages/UserPages/EmployeeLeaveRequests";
import LeaveTypes from "../Pages/DashboardPages/LeaveTypes";
import LeaveAllocations from "../Pages/DashboardPages/LeaveAllocations";
import Employees from "../Pages/DashboardPages/Employees";
import Dashboard from "../Pages/DashboardPages/Dashboard";
import NotFound from "../Pages/Shared/NotFound";
import AdminLeaveAllocations from "../Pages/DashboardPages/AdminLeaveAllocations";

const AppRoutes = () => {
    return (
        <Router>
            <Routes>
                <Route path="/signup" element={<SignUp />} />

                 {/* Fallback URL */}
                <Route path="*" element={<NotFound />} />

                 {/* User Section */}
                <Route element={<ProtectedRoute />} >
                    <Route element={<UserLayout />}>
                        <Route path="/" element={<Index />} />
                        <Route path="/leave-requests" element={<EmployeeLeaveRequests />} />                        
                    </Route>
                </Route>

                {/* Admin Section */}
                <Route element={<ProtectedRoute role="Admin" />}>
                    <Route element={<AdminLayout />}>
                        <Route path="/dashboard" element={<Dashboard />} />
                        <Route path="/dashboard/leave-types" element={<LeaveTypes />} />
                        <Route path="/dashboard/leave-requests" element={<AdminLeaveRequests />} />
                        <Route path="/dashboard/leave-allocations" element={<AdminLeaveAllocations />} />
                        <Route path="/dashboard/leave-employees" element={<Employees />} />
                    </Route>
                </Route>
            </Routes>
        </Router>
    )

}
export default AppRoutes;