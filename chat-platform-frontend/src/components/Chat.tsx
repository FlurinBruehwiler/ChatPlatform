import IChat from "../models/IChat";
import React, {ChangeEvent, useRef, useState} from "react";
import Message from "./Message";
import { SignalRService } from "../services/signalRService";
import IMessage from "../models/IMessage";
import instance from "../axios";

interface ChatProps {
  Chat: IChat;
  SignalRService: SignalRService;
  ReceiveMessage: (message: IMessage) => void;
}

function Chat(props: ChatProps) {
  const [messageContent, setMessageContent] = useState<string>("");
  const [username, setUsername] = useState<string>("");
  const [file, setFile] = useState<File>();

  const handleFileChange = (e: ChangeEvent<HTMLInputElement>) => {
    if (e.target.files) {
      setFile(e.target.files[0]);
    }
  };

  const handleUploadClick = async () => {
    if (!file) {
      return;
    }
    console.log("uploading file");

    let name = crypto.randomUUID();

    const formData = new FormData();
    formData.append("name", file.name);
    formData.append("file", file);

    let re = /(?:\.([^.]+))?$/;
    // @ts-ignore
    let extension = re.exec(file.name)[1];

    name = name + "." + extension

    await instance.post(`/upload?name=${name}`, formData)

    setFile(undefined);


    return name;
  };

  const sendMessage = async (event: { preventDefault: () => void }) => {
    event.preventDefault();
    let res = await handleUploadClick();
    if (messageContent.trim().length === 0) return;
    setMessageContent("");
    await props.SignalRService.sendMessage(props.Chat.chatId, messageContent, res);
  };

  const addUser = async () => {
    if (username.trim().length === 0) return;
    setUsername("");
    await props.SignalRService.addUserToChat(props.Chat.chatId, username);
  }

  return (
    <div
      className={"flex flex-col flex-grow-[1] justify-between h-full right-0"}
    >
      <div className={"bg-blue-50 flex flex-row p-3 items-center justify-between"}>
        <div className={"flex items-center"}>
          <img
              src="https://randomuser.me/api/portraits/women/40.jpg"
              alt=""
              className={"h-14 w-14 rounded-full"}
          />
          <div className={"text-2xl ml-4"}>{props.Chat.name}</div>
        </div>
        <div className={"flex gap-3"}>
          <input
              onChange={(e: React.FormEvent<HTMLInputElement>) =>
                  setUsername(e.currentTarget.value)
              }
              value={username}
              name="sendText"
              placeholder="username"
              className="px-3 py-3 text-blue-800 border border-blue-300 rounded-md bg-blue-50 focus:outline-none focus:ring-1 focus:ring-blue-300"
          />
          <button
              onClick={addUser}
              type="button"
              className="px-8 py-3 text-blue-100 bg-blue-600 rounded-md"
          >
            Add User
          </button>
        </div>
      </div>
      <div className={"bg-white h-full overflow-auto"}>
        <ul className={"flex flex-col"}>
          {props.Chat.messages.map((message) => (
            <Message Message={message} key={message.messageId} />
          ))}
        </ul>
      </div>
      <form onSubmit={sendMessage} className={"flex flex-row m-5 gap-x-5"}>
        <input
          onChange={(e: React.FormEvent<HTMLInputElement>) =>
            setMessageContent(e.currentTarget.value)
          }
          value={messageContent}
          name="sendText"
          placeholder="content"
          className="w-full px-3 py-2 text-blue-800 border border-blue-300 rounded-md bg-blue-50 focus:outline-none focus:ring-1 focus:ring-blue-300"
        />
        <input type={"file"} onChange={handleFileChange} accept="image/png, image/jpeg" className={"rounded-full bg-blue-600 w-16 h-11"} />

        <button
          onClick={sendMessage}
          className={"rounded-full bg-blue-600 w-16 h-11"}
        >Send</button>
      </form>
    </div>
  );
}

export default Chat;
