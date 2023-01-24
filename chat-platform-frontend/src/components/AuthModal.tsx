import React, {useState} from "react";
import {login, register} from "../services/apiService";

interface AuthModalProps {
    CloseCallback: () => void;
}

function AuthModal(props: AuthModalProps) {
    const [isRegistering, setIsRegistering] = useState<boolean>(false);

    const [username, setUsername] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const [error, setError] = useState("");

    const loginPress = async (event: { preventDefault: () => void }) => {
        event.preventDefault();
        const res = isRegistering
            ? await register(username, password)
            : await login(username, password);
        if (typeof res === "string") {
            setError(res);
            return;
        }

        props.CloseCallback();
    };

    return (
        <div className="absolute inset-0 flex items-center justify-center bg-gray-700 bg-opacity-50">
            <div className="flex flex-col w-full p-20 m-8 bg-white rounded-md lg:m-0 sm:p-10 max-w-[500px]">
                <div className="mb-8 text-center">
                    <h1 className="my-3 text-4xl font-bold">
                        {isRegistering ? "Sign up" : "Sign in"}
                    </h1>
                    <p className="text-sm text-coolGray-600">
                        {isRegistering
                            ? "Sign up to create an account"
                            : "Sign in to access your account"}{" "}
                    </p>
                </div>
                <form>
                    <div className="space-y-4">
                        <div>
                            <label className="block mb-2 text-sm">Username</label>
                            <input
                                onChange={(e: React.FormEvent<HTMLInputElement>) =>
                                    setUsername(e.currentTarget.value)
                                }
                                value={username}
                                name="username"
                                placeholder="username"
                                className="w-full px-3 py-2 text-blue-800 border border-blue-300 rounded-md bg-blue-50 focus:outline-none focus:ring-1 focus:ring-blue-300"
                            />
                        </div>
                        <div>
                            <div className="flex justify-between mb-2">
                                <label htmlFor="password" className="text-sm">
                                    Password
                                </label>
                            </div>
                            <input
                                onChange={(e: React.FormEvent<HTMLInputElement>) =>
                                    setPassword(e.currentTarget.value)
                                }
                                value={password}
                                type="password"
                                name="password"
                                placeholder="password"
                                className="w-full px-3 py-2 text-blue-800 border border-blue-300 rounded-md bg-blue-50 focus:outline-none focus:ring-1 focus:ring-blue-300"
                            />
                        </div>
                    </div>
                    <div className="mt-6 space-y-2">
                        <div className="flex gap-x-2">
                            <button
                                type="button"
                                className="w-full px-8 py-3 text-blue-100 bg-blue-600 rounded-md"
                                onClick={loginPress}
                            >
                                {isRegistering ? "Sign up" : "Sign in"}
                            </button>
                        </div>
                        <p className="px-6 text-sm text-center text-coolGray-600">
                            {isRegistering
                                ? "Already have an account?"
                                : "Don't have an account yet?"}
                            <button
                                type="button"
                                onClick={() => setIsRegistering(!isRegistering)}
                                className="text-blue-600 hover:underline ml-1"
                            >
                                {isRegistering ? "Sign in" : "Sign up"}
                            </button>
                            .
                        </p>
                        <p className={"text-red-600"}>{error}</p>
                    </div>
                </form>
            </div>
        </div>
    );
}

export default AuthModal;
