import { PropsWithChildren, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import { useRefreshTokenMutation } from "../Services/Auth.Service";
import { setLogedInUser } from '../Redux/Slices/LogedInUserSlice';
import { RootState } from "../Redux/Store/Store";
import { ApiResponse } from '../Types/ApiResponse';
import { ILogedInUserSlice } from '../Types/LogedInUser';

type ProtectedRouteProps = PropsWithChildren;


const ProtectedRoute = ({ children }: ProtectedRouteProps) => {
    const LogedInUser = useSelector((state: RootState) => state.LogedInUser);
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const [refresh, { isError, error }] = useRefreshTokenMutation();

    useEffect(() => {
        if (LogedInUser?.accessToken === "" || LogedInUser?.accessToken === undefined || LogedInUser?.accessToken === null) {
            if (LogedInUser.refreshToken !== "") {
                refresh(LogedInUser.refreshToken).then(x => {
                    const response = x.data as ApiResponse<ILogedInUserSlice>

                    if (response.isSuccess) {
                        dispatch(setLogedInUser(response.data));
                    } else {
                        navigate("/SignIn");
                    }
                })
            } else {
                navigate("/SignUp");
            }
        }
    }, [LogedInUser])
    return children;

}
export default ProtectedRoute;