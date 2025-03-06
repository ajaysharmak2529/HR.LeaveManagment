import { PropsWithChildren, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import { useRefreshTokenMutation } from "../Services/Auth.Service";
import { setLogedInUser } from '../Redux/Slices/LogedInUserSlice';

type ProtectedRouteProps = PropsWithChildren;


const ProtectedRoute = ({ children }: ProtectedRouteProps) => {
    const LogedInUser = useSelector((state: any) => state.LogedInUser);
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const [refresh, { isError, error }] = useRefreshTokenMutation();

    useEffect(() => {
        if (LogedInUser?.accessToken === "" || LogedInUser?.accessToken === undefined || LogedInUser?.accessToken === null) {
            if (LogedInUser.refreshToken !== "") {
                refresh(LogedInUser.refreshToken).then(x => {
                    dispatch(setLogedInUser(x.data));
                })
            } else {
                navigate("/SignUp");
            }
        }
    }, [LogedInUser])
    return children;

}
export default ProtectedRoute;