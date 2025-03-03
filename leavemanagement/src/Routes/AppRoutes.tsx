import { BrowserRouter as Router, Route, Routes } from "react-router-dom"
import ProtectedRoute from "./ProtectedRoute";
import LeaveType from "../Pages/LeaveType";
import SignUp from "../Pages/SignUp.1";

const AppRoutes = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<ProtectedRoute><LeaveType /></ProtectedRoute>} />                
                <Route path="/SignUp" element={<SignUp />} />
            </Routes>
        </Router>
    )

}
export default AppRoutes;