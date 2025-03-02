import { ReactNode, PropsWithChildren, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { ILogedInUser } from '../Types/LogedInUser';

type ProtectedRouteProps = PropsWithChildren;


const ProtectedRoute = ({ children }: ProtectedRouteProps) =>
{
    const LogedInUser: ILogedInUser = useSelector((state: any) => state.LogedInUser);
    const navigate = useNavigate();

    useEffect(() => {
        if (LogedInUser.accessToken === "" || LogedInUser.accessToken === null) {
            navigate("/Login");
        }
    },[])
    return children;

}
export default ProtectedRoute;