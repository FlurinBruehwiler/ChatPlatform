import TestChats from "./examples/TestChats";
import { useEffect } from "react";
import TestChat from "./examples/TestChat";
import LoginRegister from "./LoginRegister";
import Chat from "./Chat";
import ChatSelection from "./ChatSelection";

function App() {
  return (
    <div className={"grid grid-cols-main h-full"}>
        <TestChats></TestChats>
    </div>
  );
}

export default App;
