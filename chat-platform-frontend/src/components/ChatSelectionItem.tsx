import IChat from "../models/IChat";
import React from "react";

interface ChatSelectionItemProps {
  Chat: IChat;
  ClickCallback: () => void;
  IsSelectedChat: boolean;
  LeaveCallback: () => void;
}

function ChatSelectionItem(props: ChatSelectionItemProps) {
  return (
    <li
      onClick={props.ClickCallback}
      className={`p-3 ${
        props.IsSelectedChat ? "bg-blue-200" : "bg-blue-50"
      } rounded-md h-20 flex items-center mb-3 ml-0 cursor-pointer`}
    >
      <img
        src="https://randomuser.me/api/portraits/women/40.jpg"
        alt=""
        className={"h-14 w-14 rounded-full"}
      />
      <div className={"w-full ml-4"}>
        <p className={"text-xl"}>{props.Chat.name}</p>
      </div>
      <button
        onClick={props.LeaveCallback}
        type="button"
        className="text-blue-100 bg-red-600 rounded-md text-sm p-2"
      >
        Leave
      </button>
    </li>
  );
}

export default ChatSelectionItem;
