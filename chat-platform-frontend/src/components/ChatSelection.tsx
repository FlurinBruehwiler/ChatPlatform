import React, {useEffect, useState} from "react";
import IChat from "../models/IChat";
import {SignalRService} from "../services/signalRService";
import ChatSelectionItem from "./ChatSelectionItem";
import CreateChatModal from "./CreateChatModal";
import Chat from "./Chat";

interface ChatSelectionProps {
    Logout: () => void;
}

function ChatSelection(props: ChatSelectionProps) {
    const [signalRService, setSignalRService] = useState<SignalRService>(new SignalRService());

    const [chats, setChats] = useState<IChat[]>([]);
    const [chatName, setChatName] = useState<string>("");

    const [showingCreateChatModal, setShowingCreateChatModal] = useState<boolean>(false);

    const [selectedChat, setSelectedChat] = useState<IChat | undefined>();

    useEffect(() => {
        connect().then();
    }, []);

    const connect = async () => {
        await signalRService?.connect();
        let newChats: IChat[] = await signalRService?.getChats();
        if (newChats == undefined)
            return;
        setChats(newChats);
        if (newChats.length > 0)
            setSelectedChat(newChats[0]);
    }

    return (
        <div className={"bg-white w-full h-full flex"}>
            <div className={"w-[600px] p-10 overflow-auto"}>
                <div className={"mb-5"}>
                    <div className={"flex gap-x-2"}>
                        <button onClick={props.Logout} type="button"
                                className="w-full px-8 py-3 text-blue-100 bg-blue-600 rounded-md">
                            Log out
                        </button>
                        <button onClick={signalRService?.connect} type="button"
                                className="w-full px-8 py-3 text-blue-100 bg-blue-600 rounded-md">
                            Connect
                        </button>
                        <button onClick={() => setShowingCreateChatModal(true)} type="button"
                                className="w-full px-8 py-3 text-blue-100 bg-blue-600 rounded-md">
                            Create Chat
                        </button>
                        {
                            showingCreateChatModal ?
                                <CreateChatModal CloseCallback={() => setShowingCreateChatModal(false)}
                                                 SignalRService={signalRService}
                                                 CreateChatCallback={(newChat) => setChats(oldChats => [...oldChats, newChat])}/> : null
                        }
                    </div>
                </div>
                <ul className={"overflow-auto"}>
                    {
                        chats.map((chat) => <ChatSelectionItem Chat={chat} key={chat.chatId}
                                                               ClickCallback={() => setSelectedChat(chat)}
                                                               IsSelectedChat={selectedChat === chat}/>)
                    }
                </ul>
            </div>
            {selectedChat ? <Chat Chat={selectedChat}/> : null}
        </div>
    );
}

export default ChatSelection;
