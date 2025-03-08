import { Outlet } from 'react-router-dom';
import HeaderNav from '../../Components/UserHeaderNav';

const UserLayout = () => {
    return (
        <>
            <HeaderNav />
            <Outlet />
        </>

    );
};

export default UserLayout;