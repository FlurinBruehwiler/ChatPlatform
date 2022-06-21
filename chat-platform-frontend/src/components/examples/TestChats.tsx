import React, { useEffect, useState } from "react";
import IChat from "../../models/IChat";
import { login, register } from "../../services/apiService";
import IApiError from "../../models/IApiError";
import { SignalRService } from "../../services/signalRService";

function TestChats() {
  const [signalRService, setSignalRService] = useState<
    SignalRService | undefined
  >(undefined);

  const [chats, setChats] = useState<IChat[]>([]);

  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");

  const [chatName, setChatName] = useState<string>("");

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

  const createChatPress = async () => {
    if (!signalRService) return;
    signalRService.createChat(chatName);

  };

  const usernameChange = (e: React.FormEvent<HTMLInputElement>) => {
    setUsername(e.currentTarget.value);
  };

  const passwordChange = (e: React.FormEvent<HTMLInputElement>) => {
    setPassword(e.currentTarget.value);
  };

  const chatNameChange = (e: React.FormEvent<HTMLInputElement>) => {
    setChatName(e.currentTarget.value);
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
      <div>
        <input
          type="text"
          onChange={chatNameChange}
          value={chatName}
          className={"border-black border-2"}
        />
        <button onClick={createChatPress}>Create Chat</button>
      </div>
    </div>
  );
}

export default TestChats;
