import TestChats from "./examples/TestChats";
import { useEffect } from "react";
import TestChat from "./examples/TestChat";
import LoginRegister from "./LoginRegister";
import Chat from "./Chat";
import ChatSelection from "./ChatSelection";

function App() {
  useEffect(() => {
    console.log("app ctor");
  }, []);

  return (
    <div className={"grid grid-cols-main h-full"}>
     <LoginRegister />
     <ChatSelection />
    </div>
  );
}

export default App;
