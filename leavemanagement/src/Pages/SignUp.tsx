import { useState, FormEvent, ChangeEvent } from "react";
import { useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";
import { setLogedInUser } from "../Redux/Slices/LogedInUserSlice"
import { useLoginMutation, useSignupMutation } from "../Services/Auth.Service";
import { ISignUp, ILogin, ILogedInUserSlice } from "../Types/LogedInUser"

interface State { sigin: ILogin, signup: ISignUp }
const initialState: State = { sigin: { email: "", password: "" }, signup: { email: "", firstName: "", lastName: "", userName:"", password: "", confirmPassword: "" } };

const SignUp = () => {

    const [tab, setTab] = useState('signin');
    const [state, setState] = useState<State>(initialState);
    const [login, { isLoading:isLoginLoading, isError:isLoginError, error:loginError }] = useLoginMutation();
    const [signup, { isLoading:isSignUpLoading, isError:isSignUpError, error:signUpError }] = useSignupMutation();
    const dispatch = useDispatch()
    const navigate = useNavigate()

    const handelFormSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        if (tab === 'signin') {
            login(state?.sigin).then(x => {
                dispatch(setLogedInUser(x.data));
                navigate("/");

            });
            console.log(signUpError);
        } else {
            signup(state.signup).then(x => {
                dispatch(setLogedInUser(x.data));
                navigate("/"); });

        }
    };
    const handelOnChange = (e: ChangeEvent<HTMLInputElement>): void => {
        if (tab === 'signin') {
            setState({ ...state, sigin: { ...state.sigin, [e.target.name]: e.target.value } });
        }
        else {
            setState({ ...state, signup: { ...state.signup, [e.target.name]: e.target.value } });
        }
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

                {
                    (isLoginError || isSignUpError) && (
                        <span className="inline-flex items-center px-3 py-1 rounded-full text-sm font-medium bg-red-100 text-red-800 dark:bg-red-900 dark:text-red-300">
                            Error Temp                    
                </span>)}
                {/* Sign In Form */}
                {tab === "signin" && (
                    <form className="space-y-4" onSubmit={handelFormSubmit}>
                        <div>
                            <label className="block text-gray-700 font-medium mb-1">Email</label>
                            <input type="email"
                                name="email"
                                className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500"
                                placeholder="Ex. rajesh@email.com"
                                onChange={handelOnChange}
                            />
                        </div>
                        <div>
                            <label className="block text-gray-700 font-medium mb-1">Password</label>
                            <input type="password" name="password"
                                className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500"
                                placeholder="********"
                                onChange={handelOnChange}
                            />
                        </div>
                        {/*<div className="flex justify-between text-sm text-gray-600">*/}
                        {/*    <label className="flex items-center space-x-2">*/}
                        {/*        <input type="checkbox" className="h-4 w-4 text-indigo-600 border-gray-300 rounded" />*/}
                        {/*        <span>Remember me</span>*/}
                        {/*    </label>*/}
                        {/*    <a href="#" className="text-indigo-600 hover:underline">Forgot password?</a>*/}
                        {/*</div>*/}
                        <button className="w-full bg-indigo-600 hover:bg-indigo-700 text-white font-medium py-3 rounded-lg shadow-md hover:shadow-lg transition" disabled={isLoginLoading}>{isLoginLoading ? "Prosessing ..." : "Sign In"}</button>
                    </form>
                )}

                {/* Sign Up Form */}
                {tab === "signup" && (
                    <form className="space-y-2" onSubmit={handelFormSubmit}>
                        <div>
                            <label className="block text-gray-700 font-medium mb-1">First Name</label>
                            <input
                                type="text"
                                name="firstName"
                                className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500"
                                placeholder="Ex. Rajesh"
                                onChange={handelOnChange}
                            />
                        </div>
                        <div>
                            <label className="block text-gray-700 font-medium mb-1">Last Name</label>
                            <input
                                type="text"
                                name="lastName"
                                className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500"
                                placeholder="Ex. Maheshwari"
                                onChange={handelOnChange}
                            />
                        </div>
                        <div>
                            <label className="block text-gray-700 font-medium mb-1">Username</label>
                            <input
                                type="text"
                                name="username"
                                className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500"
                                placeholder="Ex. Rajesh_Maheshwari_2528"
                                onChange={handelOnChange}
                            />
                        </div>
                        <div>
                            <label className="block text-gray-700 font-medium mb-1">Email</label>
                            <input
                                type="email"
                                name="email"
                                className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500"
                                placeholder="Ex. rajesh@email.com"
                                onChange={handelOnChange}
                            />
                        </div>
                        <div>
                            <label className="block text-gray-700 font-medium mb-1">Password</label>
                            <input type="password" name="password"
                                className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500"
                                placeholder="******"
                                onChange={handelOnChange}
                            />
                        </div>
                        <div>
                            <label className="block text-gray-700 font-medium mb-1">Confirm Password</label>
                            <input type="password" name="confirmPassword"
                                className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500"
                                placeholder="******"
                                onChange={handelOnChange}
                            />
                        </div>
                        <button className="w-full mx-auto bg-indigo-600 hover:bg-indigo-700 text-white font-medium py-3 rounded-lg shadow-md hover:shadow-lg transition">{isSignUpLoading?"Prosessing ...": "Sign Up"}</button>
                    </form>
                )}                
            </div>
        </div>
    );
};
export default SignUp;