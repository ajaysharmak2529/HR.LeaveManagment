import { Link } from 'react-router-dom'
import { useSelector } from "react-redux"
import { RootState } from '../Redux/Store/Store'
import { ThemeToggleButton } from './ThemeToggleButton';
import UserDropdown from './UserDropdown';
import { GiHamburgerMenu } from "react-icons/gi";

const HeaderNav = () => {
    const { accessToken } = useSelector((state: RootState) => state.LogedInUser);
    return (
        <nav className="flex justify-between dark:bg-gray-900 bg-white border-gray-200 dark:border-gray-800 dark:text-white w-screen">
            <div className="px-5 xl:px-12 py-6 flex w-full items-center">
                <Link className='text-3xl font-bold font-heading' to="/">Logo Here.</Link>
                <ul className="hidden md:flex px-4 mx-auto font-semibold font-heading space-x-12">
                    <li><Link className="hover:text-gray-200" to="/">Home</Link></li>
                    <li><Link className="hover:text-gray-200" to="/leave-requests">Leave Requests</Link></li>
                </ul>
                <div className="hidden xl:flex items-center space-x-5 items-center">
                    <div>
                        {
                            accessToken !== "" ?
                                <div className="flex gap-4">  
                                    <ThemeToggleButton />
                                    <UserDropdown/>
                                </div> :
                                <Link to="/signup" className="py-2.5 px-6 rounded-lg text-sm font-medium bg-teal-200 text-teal-800">Login/Register</Link>
                        }
                    </div>

                </div>
            </div>            
            <a className="navbar-burger self-center mr-12 xl:hidden" href="#">
                <GiHamburgerMenu className="h-6 w-6 hover:text-gray-200" />
            </a>
        </nav>
    )
}

export default HeaderNav