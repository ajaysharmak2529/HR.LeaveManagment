import { BrowserRouter as Router, Route, Routes } from "react-router-dom"
import ProtectedRoute from "./ProtectedRoute";
import Login from "../Pages/Login";
import LeaveType from "../Pages/LeaveType";

const AppRoutes = () => {
    return (
        <Router>
            <Routes>
                <Route path="/LeaveType" element={<ProtectedRoute><LeaveType /></ProtectedRoute>} />
                <Route path="/Login" element={<Login />} />
            </Routes>
        </Router>
    )

}
export default AppRoutes;