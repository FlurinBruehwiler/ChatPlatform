import ChatSelectionItem from "./ChatSelectionItem";
import React, {useEffect, useState} from "react";
import IChat from "../models/IChat";
import {SignalRService} from "../services/signalRService";

function ChatSelection() {
  const [signalRService, setSignalRService] = useState<
      SignalRService | undefined
      >(undefined);

  const [chats, setChats] = useState<IChat[]>([]);
  const [chatName, setChatName] = useState<string>("");

  useEffect(() => {
    console.log("ctor");
    setSignalRService(new SignalRService());
  }, []);
  const createChatPress = async () => {
    if (!signalRService) return;
    await signalRService.createChat(chatName);
  };

  const addUsertoChat = async () => {

  }

  const chatNameChange = (e: React.FormEvent<HTMLInputElement>) => {
    setChatName(e.currentTarget.value);
  };
  return (
    <div className={"bg-gray-500 w-full overflow-auto h-full"}>
      <ul className={"overflow-auto p-3 h-full"}>
        <input
            type="text"
            onChange={chatNameChange}
            value={chatName}
            className={"border-black border-2"}
        />
        <button onClick={createChatPress}>Create Chat</button>
        <button onClick={addUsertoChat}>Add User</button>
      </ul>
    </div>
  );
}

export default ChatSelection;
