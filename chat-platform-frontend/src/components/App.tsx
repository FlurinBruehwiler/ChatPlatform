import ChatSelection from "./ChatSelection";
import Chat from "./Chat";

function App() {
  return (
    <div className={"grid grid-cols-main h-full"}>
      <ChatSelection></ChatSelection>
      <Chat></Chat>
    </div>
  );
}

export default App;
