import { useEffect } from 'react';
import { useNavigate, Outlet, Navigate } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import { useRefreshTokenMutation } from "../Services/Auth.Service";
import { setLogedInUser } from '../Redux/Slices/LogedInUserSlice';
import { RootState } from "../Redux/Store/Store";
import { ApiResponse } from '../Types/ApiResponse';
import { ILogedInUserSlice } from '../Types/LogedInUser';
import Loader from '../Components/Loader';

interface ProtectedRouteProps {
    role?: 'admin'
}


const ProtectedRoute = ({ role }: ProtectedRouteProps) => {
    const LogedInUser = useSelector((state: RootState) => state.LogedInUser);
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const [refresh, { isLoading, isError, error }] = useRefreshTokenMutation();


    useEffect(() => {
        if (LogedInUser?.accessToken === "" || LogedInUser?.accessToken === undefined || LogedInUser?.accessToken === null) {
        if (LogedInUser.refreshToken !== "") {
            refresh(LogedInUser.refreshToken).then(x => {
                const response = x.data as ApiResponse<ILogedInUserSlice>

                if (response.isSuccess) {
                    dispatch(setLogedInUser(response.data));
                } else {
                    navigate("/SignUp", { replace:true });
                }
            })
        } else {
            navigate("/SignUp", { replace: true });
        }
    }
    }, [dispatch])

    

    if (isLoading) return <Loader />
    if (isError) {
        console.log(error)
        return <Navigate to="/SignUp" replace={true} />
    };

    return <Outlet />;

}
export default ProtectedRoute;