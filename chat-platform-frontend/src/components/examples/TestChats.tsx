import React, { useEffect, useState } from "react";
import IChat from "../../models/IChat";
import { login, register } from "../../services/apiService";
import IApiError from "../../models/IApiError";
import { SignalRService } from "../../services/signalRService";
import {sign} from "crypto";
import TestChat from "./TestChat";

function TestChats() {
  const [signalRService, setSignalRService] = useState<
    SignalRService | undefined
  >(undefined);

  const [chats, setChats] = useState<IChat[]>([]);
  const [selectedChat, setSelectedChat] = useState<string>();

  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");

  const [chatName, setChatName] = useState<string>("");

  useEffect(() => {
    setSignalRService(new SignalRService());
    connect().then();
  }, []);

  const connect = async () => {
    await signalRService?.connect();
    let newChats = await signalRService?.getChats();
    if(newChats == undefined)
      return;
    setChats(newChats);
  }

  const registerPress = async () => {
    await register(username, password, (error: IApiError) => {});
  };

  const loginPress = async () => {
    await login(username, password, (error: IApiError) => {});
    await connect();
  };

  const createChatPress = async () => {
    if (!signalRService) return;
    let newChat = await signalRService.createChat(chatName);
    setChats(oldChats => [...oldChats, newChat]);
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
      <button onClick={connect}>Connect</button>
      <div>
        <input
          type="text"
          onChange={chatNameChange}
          value={chatName}
          className={"border-black border-2"}
        />
        <button onClick={createChatPress}>Create Chat</button>
      </div>
      <div className={"w-full border-2 border-black"}></div>
      <select name="chats" id="chats" className={"w-32"} value={selectedChat}>
        {
          chats.map((chat) => {
            console.log(JSON.stringify(chat));
            return <option key={chat.chatdId} value={chat.chatdId}>{chat.name}</option>
          }
        )}
      </select>
      <TestChat chat={chats[0]}></TestChat>
    </div>
  );
}

export default TestChats;
