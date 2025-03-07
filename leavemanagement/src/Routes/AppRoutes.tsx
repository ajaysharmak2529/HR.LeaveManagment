import { BrowserRouter as Router, Route, Routes } from "react-router-dom"
import ProtectedRoute from "./ProtectedRoute";
import LeaveType from "../Pages/LeaveType";
import SignUp from "../Pages/SignUp";
import HeaderNav from "../Components/HeaderNav";

const AppRoutes = () => {
    return (
        <Router>
            <HeaderNav/>
            <Routes>
                <Route path="/" element={<ProtectedRoute><LeaveType /></ProtectedRoute>} />                
                <Route path="/signup" element={<SignUp />} />
            </Routes>
        </Router>
    )

}
export default AppRoutes;