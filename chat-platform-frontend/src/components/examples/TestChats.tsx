import React, { useState } from "react";
import IChat from "../../models/IChat";
import { login, register } from "../../services/apiService";
import IApiError from "../../models/IApiError";

function TestChats() {
  const [chats, setChats] = useState<IChat[]>([]);

  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");

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
      <div className={"w-full border-2 border-black"}></div>
    </div>
  );
}

export default TestChats;
