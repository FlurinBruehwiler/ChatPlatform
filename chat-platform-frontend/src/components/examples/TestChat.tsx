import { useState } from "react";
import IUser from "../../models/IUser";
import IMessage from "../../models/IMessage";

function TestChat() {
  const [messages, setMessages] = useState<IMessage[]>([]);
  const [users, setUsers] = useState<IUser[]>([]);

  return <div></div>;
}

export default TestChat;
