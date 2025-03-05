import { PropsWithChildren, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useSelector } from 'react-redux';

type ProtectedRouteProps = PropsWithChildren;


const ProtectedRoute = ({ children }: ProtectedRouteProps) => {
    const LogedInUser = useSelector((state: any) => state.LogedInUser);
    const navigate = useNavigate();

    useEffect(() => {
        console.log(LogedInUser)
        if (LogedInUser?.accessToken === "" || LogedInUser?.accessToken === null) {
            navigate("/SignUp");
        }
    }, [LogedInUser])
    return children;

}
export default ProtectedRoute;