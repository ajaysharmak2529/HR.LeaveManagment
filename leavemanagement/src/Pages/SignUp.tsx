import { useState, FormEventHandler } from "react";

export const SignUp = () => {

    const [tab, setTab] = useState('signin');
    const handelSignin: FormEventHandler<HTMLFormElement> = (e) => {
        e.preventDefault();
        const formData = new FormData(e.currentTarget);
        let data = Object.fromEntries(formData.entries());
        console.log(data);
    };
    const handelSignup = (e: any) => {
        e.preventDefault();
    };

    return (
        <div className="bg-black flex items-center justify-center min-h-screen p-4">
            <div className="w-full max-w-md bg-white shadow-lg rounded-2xl p-6 md:p-8 transition-all duration-300">
                {/* Tabs */}
                <div className="flex justify-between mb-6 border-b border-gray-200">
                    <button
                        onClick={() => setTab("signin")}
                        className={`w-1/2 text-center pb-2 font-medium border-b-2 transition focus:outline-none ${tab === "signin" ? "text-indigo-600 border-indigo-500" : "text-gray-600 border-transparent hover:border-indigo-500"}`}
                    >
                        Sign In
                    </button>
                    <button
                        onClick={() => setTab("signup")}
                        className={`w-1/2 text-center pb-2 font-medium border-b-2 transition focus:outline-none ${tab === "signup" ? "text-indigo-600 border-indigo-500" : "text-gray-600 border-transparent hover:border-indigo-500"}`}
                    >
                        Sign Up
                    </button>
                </div>

                {/* Sign In Form */}
                {tab === "signin" && (
                    <form className="space-y-4" onSubmit={handelSignin}>
                        <div>
                            <label className="block text-gray-700 font-medium mb-1">Email</label>
                            <input type="email" name="email" className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500" placeholder="rajesh@email.com" />
                        </div>
                        <div>
                            <label className="block text-gray-700 font-medium mb-1">Password</label>
                            <input type="password" name="password" className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500" placeholder="********" />
                        </div>
                        <div className="flex justify-between text-sm text-gray-600">
                            <label className="flex items-center space-x-2">
                                <input type="checkbox" className="h-4 w-4 text-indigo-600 border-gray-300 rounded" />
                                <span>Remember me</span>
                            </label>
                            <a href="#" className="text-indigo-600 hover:underline">Forgot password?</a>
                        </div>
                        <button className="w-full bg-indigo-600 hover:bg-indigo-700 text-white font-medium py-3 rounded-lg shadow-md hover:shadow-lg transition">Sign In</button>
                    </form>
                )}

                {/* Sign Up Form */}
                {tab === "signup" && (
                    <form className="space-y-4" onSubmit={handelSignup}>
                        <div>
                            <label className="block text-gray-700 font-medium mb-1">First Name</label>
                            <input type="text" name="firstName" className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500" placeholder="Rajesh Maheshwari" />
                        </div>
                        <div>
                            <label className="block text-gray-700 font-medium mb-1">Last Name</label>
                            <input type="text" name="lastName" className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500" placeholder="Rajesh Maheshwari" />
                        </div>
                        <div>
                            <label className="block text-gray-700 font-medium mb-1">Email</label>
                            <input type="email" name="email" className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500" placeholder="rajesh@email.com" />
                        </div>
                        <div>
                            <label className="block text-gray-700 font-medium mb-1">Password</label>
                            <input type="password" name="password" className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500" placeholder="******" />
                        </div>
                        <div>
                            <label className="block text-gray-700 font-medium mb-1">Confirm Password</label>
                            <input type="password" name="confirmPassword" className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500" placeholder="******" />
                        </div>
                        <button className="w-full bg-indigo-600 hover:bg-indigo-700 text-white font-medium py-3 rounded-lg shadow-md hover:shadow-lg transition">Sign Up</button>
                    </form>
                )}

                {/* Social Login */}
                <div className="mt-6">
                    <p className="text-center text-gray-500">Or continue with</p>
                    <div className="mt-4 flex space-x-3">
                        <button className="flex-1 bg-gray-100 hover:bg-gray-200 transition p-3 rounded-lg flex items-center justify-center">
                            <img src="https://upload.wikimedia.org/wikipedia/commons/0/05/Facebook_Logo_%282019%29.png" className="h-5" alt="Facebook" />
                        </button>
                        <button className="flex-1 bg-gray-100 hover:bg-gray-200 transition p-3 rounded-lg flex items-center justify-center">
                            <img src="https://upload.wikimedia.org/wikipedia/commons/9/91/Octicons-mark-github.svg" className="h-5" alt="Github" />
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
};
