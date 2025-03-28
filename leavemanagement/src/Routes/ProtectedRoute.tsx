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
    role?: 'Admin' | string
}


const ProtectedRoute = ({ role }: ProtectedRouteProps) => {
    const { accessToken, refreshToken, roles } = useSelector((state: RootState) => state.LogedInUser);
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const [refresh, { isLoading, isError, error }] = useRefreshTokenMutation();


    useEffect(() => {
        if (accessToken === "" || accessToken === undefined || accessToken === null) {
            if (refreshToken !== "") {
                refresh(refreshToken).then(({ data, error }) => {

                    if (data) {
                        const response = data as ApiResponse<ILogedInUserSlice>

                        if (response.isSuccess) {
                            dispatch(setLogedInUser(response.data));
                        } else {
                            navigate("/SignUp", { replace: true });
                        }
                    }
                    if (error) {
                        console.log(`Error ${(error as any).data}`)
                    }
                })
            } else {
                navigate("/SignUp", { replace: true });
            }
        }
    }, [accessToken])

    if (role !== undefined) {
        if (!roles?.includes(role)) {
            navigate("/", { replace: true });
        }
    }

    if (isLoading) return <Loader />


    if (isError) {
        console.log(error)
        return <Navigate to="/SignUp" replace={true} />
    };

    return <Outlet />;

}
export default ProtectedRoute;