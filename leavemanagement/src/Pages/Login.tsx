import { FormEventHandler, useState } from "react";

interface LoginUser {
    username: string;
    password: string;
}


const Login = () => {

    const [loginUser, setLoginUser] = useState<LoginUser>({ username: "", password: "" });

    const handelFormSubmit: FormEventHandler<HTMLFormElement> = (e) => {
        e.preventDefault();

    };
    return (
        <form onSubmit={handelFormSubmit}>
            <div className="w-screen min-h-screen flex items-center justify-center bg-gray-50 dark:bg-gray-800 px-4 sm:px-6 lg:px-8">
                <div className="relative py-3 sm:max-w-xs sm:mx-auto">
                    <div className="min-h-96 px-8 py-6 mt-4 text-left bg-white dark:bg-gray-900  rounded-xl shadow-lg">
                        <div className="flex flex-col justify-center items-center h-full select-none">
                            <div className="flex flex-col items-center justify-center gap-2 mb-8">
                                <a href="https://amethgalarcio.web.app/" target="_blank">
                                    <img src="https://th.bing.com/th/id/OIP.mgcaoJeZ7Q_IXzW7Xsgk_wAAAA?rs=1&pid=ImgDetMain" className="w-8" />
                                </a>
                                <p className="m-0 text-[16px] font-semibold dark:text-white">Login to your Account</p>
                                <span className="m-0 text-xs max-w-[90%] text-center text-[#8B8E98]">Get started with our app, just start section and enjoy experience.
                                </span>
                            </div>
                            <div className="w-full flex flex-col gap-2">
                                <label className="font-semibold text-xs text-gray-400 ">Username</label>
                                <input
                                    onClick={(e) => setLoginUser({ ...loginUser, username: (e.target as HTMLInputElement).value })}
                                    value={loginUser.username}
                                    className="border rounded-lg px-3 py-2 mb-5 text-sm w-full outline-none dark:border-gray-500 dark:bg-gray-900"
                                    placeholder="Username" />
                            </div>
                        </div>
                        <div className="w-full flex flex-col gap-2">
                            <label className="font-semibold text-xs text-gray-400 ">Password</label>
                            <input
                                onClick={(e) => setLoginUser({ ...loginUser, password: (e.target as HTMLInputElement).value })}
                                type="password"
                                className="border rounded-lg px-3 py-2 mb-5 text-sm w-full outline-none dark:border-gray-500 dark:bg-gray-900"
                                placeholder="Password" />

                        </div>
                        <div className="mt-5">
                            <button className="py-1 px-8 bg-blue-500 hover:bg-blue-800 focus:ring-offset-blue-200 text-white w-full transition ease-in duration-200 text-center text-base font-semibold shadow-md focus:outline-none focus:ring-2 focus:ring-offset-2 rounded-lg cursor-pointer select-none">Login</button>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    )
}

export default Login