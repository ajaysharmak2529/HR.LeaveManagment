import { Link } from 'react-router-dom'
import { useSelector } from "react-redux"
import { RootState } from '../Redux/Store/Store'
import { ThemeToggleButton } from './ThemeToggleButton';
import UserDropdown from './UserDropdown';


const HeaderNav = () => {
    const { accessToken, userName } = useSelector((state: RootState) => state.LogedInUser);
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
                                <p className="flex gap-4">  
                                    <ThemeToggleButton />
                                    <UserDropdown/>
                                </p> :
                                <Link to="/signup" className="py-2.5 px-6 rounded-lg text-sm font-medium bg-teal-200 text-teal-800">Login/Register</Link>
                        }
                    </div>

                </div>
            </div>
            <a className="xl:hidden flex mr-6 items-center" href="#">
                <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6 hover:text-gray-200" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 3h2l.4 2M7 13h10l4-8H5.4M7 13L5.4 5M7 13l-2.293 2.293c-.63.63-.184 1.707.707 1.707H17m0 0a2 2 0 100 4 2 2 0 000-4zm-8 2a2 2 0 11-4 0 2 2 0 014 0z" />
                </svg>
                <span className="flex absolute -mt-5 ml-4">
                    <span className="animate-ping absolute inline-flex h-3 w-3 rounded-full bg-pink-400 opacity-75"></span>
                    <span className="relative inline-flex rounded-full h-3 w-3 bg-pink-500">
                    </span>
                </span>
            </a>
            <a className="navbar-burger self-center mr-12 xl:hidden" href="#">
                <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6 hover:text-gray-200" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16M4 18h16" />
                </svg>
            </a>
        </nav>
    )
}

export default HeaderNav