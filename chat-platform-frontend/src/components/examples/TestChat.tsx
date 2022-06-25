import { useState } from "react";
import IUser from "../../models/IUser";
import IMessage from "../../models/IMessage";
import IChat from "../../models/IChat";

interface TestChatProps {
  chat: IChat
}

function TestChat(props : TestChatProps) {
  const [messages, setMessages] = useState<IMessage[]>([]);
  const [users, setUsers] = useState<IUser[]>([]);

  return <div></div>;
}

export default TestChat;
