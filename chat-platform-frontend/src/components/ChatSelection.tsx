import React, { useEffect, useState } from "react";
import IChat from "../models/IChat";
import { SignalRService } from "../services/signalRService";
import ChatSelectionItem from "./ChatSelectionItem";
import CreateChatModal from "./CreateChatModal";
import Chat from "./Chat";
import IMessage from "../models/IMessage";
import IUser from "../models/IUser";

interface ChatSelectionProps {
  Logout: () => void;
}

function ChatSelection(props: ChatSelectionProps) {
  const [signalRService, setSignalRService] = useState<SignalRService>(
    new SignalRService()
  );

  const [chats, setChats] = useState<IChat[]>([]);

  const [showingCreateChatModal, setShowingCreateChatModal] =
    useState<boolean>(false);

  const [selectedChat, setSelectedChat] = useState<IChat | undefined>();

  const [user, setUser] = useState<IUser>();

  useEffect(() => {
    if (!selectedChat) return;
    if (!chats.includes(selectedChat)) setSelectedChat(undefined);
  }, [chats]);

  useEffect(() => {
    connect().then(() => {});
  }, []);

  const connect = async () => {
    await signalRService?.connect();
    let newChats: IChat[] = await signalRService?.getChats();
    if (newChats === undefined) return;
    setChats(newChats);
    if (newChats.length > 0) setSelectedChat(newChats[0]);
    let currentUser = await signalRService.getCurrentUser();
    setUser(currentUser);
  };

  signalRService.receivedMessage = (message: IMessage) => {
    let newChats = [...chats];
    let matchingChats = newChats.filter((x) => x.chatId === message.chatId);
    if (matchingChats.length !== 1) {
      console.log("Cant find chat help me");
      return;
    }
    matchingChats[0].messages.push(message);
    setChats(newChats);
  };

  signalRService.receiveInvite = async (chatId: number) => {
    console.log("received Invite");
    let chat = await signalRService.joinChat(chatId);
    setChats((oldChats) => [...oldChats, chat]);
  };

  signalRService.receiveKick = async (chatId: number) => {
    console.log("received Kick");
    await signalRService.kickChat(chatId);
    setChats((oldChats) => {
      oldChats.splice(
        chats.indexOf(chats.filter((x) => x.chatId === chatId)[0]),
        1
      );
      return [...oldChats];
    });
  };

  return (
    <div className={"bg-white w-full h-full flex"}>
      <div className={"w-[600px] p-10 overflow-auto"}>
        <div className={"mb-5"}>
          <div className={"flex items-center mb-3 gap-x-2"}>
            <img
              src={"https://randomuser.me/api/portraits/women/40.jpg"}
              alt=""
              className={"h-14 w-14 rounded-full"}
            />
            <div className={"text-2xl"}>{user?.username}</div>
          </div>
          <div className={"flex gap-x-2"}>
            <button
              onClick={props.Logout}
              type="button"
              className="w-full px-8 py-3 text-blue-100 bg-blue-600 rounded-md"
            >
              Log out
            </button>
            <button
              onClick={() => setShowingCreateChatModal(true)}
              type="button"
              className="w-full px-8 py-3 text-blue-100 bg-blue-600 rounded-md"
            >
              Create Chat
            </button>
            {showingCreateChatModal ? (
              <CreateChatModal
                CloseCallback={() => setShowingCreateChatModal(false)}
                SignalRService={signalRService}
              />
            ) : null}
          </div>
        </div>
        <ul className={"overflow-auto"}>
          {chats.map((chat) => (
            <ChatSelectionItem
              Chat={chat}
              key={chat.chatId}
              ClickCallback={() => setSelectedChat(chat)}
              IsSelectedChat={selectedChat === chat}
              LeaveCallback={async () => {
                await signalRService.leaveChat(chat.chatId);
              }}
            />
          ))}
        </ul>
      </div>
      {selectedChat ? (
        <Chat
          Chat={selectedChat}
          SignalRService={signalRService}
          ReceiveMessage={() => {}}
        />
      ) : null}
    </div>
  );
}

export default ChatSelection;
