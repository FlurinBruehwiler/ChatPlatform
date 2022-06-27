import IChat from "../models/IChat";
import React, {useState} from "react";
import Message from "./Message";
import {SignalRService} from "../services/signalRService";
import IMessage from "../models/IMessage";

interface ChatProps {
    Chat: IChat;
    SignalRService: SignalRService;
    ReceiveMessage: (message: IMessage) => void;
}

function Chat(props: ChatProps) {
    const [messageContent, setMessageContent] = useState<string>("");

    const sendMessage = async (event: { preventDefault: () => void }) => {
        event.preventDefault();
        if (messageContent.trim().length === 0)
            return;
        setMessageContent("");
        await props.SignalRService.sendMessage(props.Chat.chatId, messageContent);
    }

    return (
        <div className={"flex flex-col flex-grow-[1] justify-between h-full right-0"}>
            <div className={"bg-blue-50 flex flex-row p-3 items-center"}>
                <img
                    src="https://randomuser.me/api/portraits/women/40.jpg"
                    alt=""
                    className={"h-14 w-14 rounded-full"}
                />
                <div className={"text-2xl ml-4"}>{props.Chat.name}</div>
            </div>
            <div className={"bg-white h-full overflow-auto"}>
                <ul className={"flex flex-col"}>
                    {
                        props.Chat.messages.map((message) => <Message Message={message} key={message.messageId}/>)
                    }
                </ul>
            </div>
            <form onSubmit={sendMessage} className={"flex flex-row m-5 gap-x-5"}>
                <input
                    onChange={(e: React.FormEvent<HTMLInputElement>) => setMessageContent(e.currentTarget.value)}
                    value={messageContent}
                    name="sendText" placeholder="content"
                    className="w-full px-3 py-2 text-blue-800 border border-blue-300 rounded-md bg-blue-50 focus:outline-none focus:ring-1 focus:ring-blue-300"/>

                <button onClick={sendMessage} className={"rounded-full bg-blue-600 w-11 h-11"}></button>
            </form>
        </div>
    );
}

export default Chat;
