import { BrowserRouter as Router, Route, Routes } from "react-router-dom"
import ProtectedRoute from "./ProtectedRoute";
import LeaveType from "../Pages/UserPages/LeaveType";
import SignUp from "../Pages/SignUp";
import UserLayout from "../Pages/Shared/UserLayout";
import AdminLayout from "../Pages/Shared/AdminLayout";
import LeaveRequest from "../Pages/UserPages/LeaveRequest";
import Index from "../Pages/UserPages/Index";
import LeaveRequests from "../Pages/UserPages/LeaveRequests";
import LeaveTypes from "../Pages/DashboardPages/LeaveTypes";
import LeaveAllocations from "../Pages/DashboardPages/LeaveAllocations";
import Employees from "../Pages/DashboardPages/Employees";
import Dashboard from "../Pages/DashboardPages/Dashboard";
import NotFound from "../Pages/Shared/NotFound";

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
                        <Route path="/leave-request" element={<LeaveRequest />} />
                        <Route path="/leave-requests" element={<LeaveRequests />} />                        
                    </Route>
                </Route>

                {/* Admin Section */}
                <Route element={<ProtectedRoute role="admin" />}>
                    <Route element={<AdminLayout />}>
                        <Route path="/dashboard" element={<Dashboard />} />
                        <Route path="/dashboard/leave-types" element={<LeaveTypes />} />
                        <Route path="/dashboard/leave-requests" element={<LeaveRequests />} />
                        <Route path="/dashboard/leave-allocations" element={<LeaveAllocations />} />
                        <Route path="/dashboard/leave-employees" element={<Employees />} />
                    </Route>
                </Route>
            </Routes>
        </Router>
    )

}
export default AppRoutes;