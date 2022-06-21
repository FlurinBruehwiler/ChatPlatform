import React, { useEffect, useState } from "react";
import {SignalRService} from "../services/signalRService";
import IApiError from "../models/IApiError";
import {login, register} from "../services/apiService";


function LoginRegister() {
    const [signalRService, setSignalRService] = useState<
        SignalRService | undefined
        >(undefined);


    const [username, setUsername] = useState<string>("");
    const [password, setPassword] = useState<string>("");


    useEffect(() => {
        console.log("ctor");
        setSignalRService(new SignalRService());
    }, []);

    const registerPress = async () => {
        await register(username, password, (error: IApiError) => {});
    };

    const loginPress = async () => {
        await login(username, password, (error: IApiError) => {});
    };

    const usernameChange = (e: React.FormEvent<HTMLInputElement>) => {
        setUsername(e.currentTarget.value);
    };

    const passwordChange = (e: React.FormEvent<HTMLInputElement>) => {
        setPassword(e.currentTarget.value);
    };


    return (
        <div>
            <div>
                <h2>Login/Register</h2>
                <div className={"flex"}>
                    <p>Username</p>
                    <input
                        type="text"
                        onChange={usernameChange}
                        value={username}
                        className={"border-black border-2"}
                    />
                </div>
                <div className={"flex"}>
                    <p>Password</p>
                    <input
                        type="text"
                        onChange={passwordChange}
                        value={password}
                        className={"border-black border-2"}
                    />
                </div>
                <button onClick={registerPress}>Register</button>
                <button onClick={loginPress}>Login</button>
            </div>
        </div>
    );
}

export default LoginRegister;