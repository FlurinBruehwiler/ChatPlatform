import ChatSelectionItem from "./ChatSelectionItem";

function ChatSelection() {
  return (
    <div className={"bg-gray-500 w-full overflow-auto h-full"}>
      <ul className={"overflow-auto p-3 h-full"}>
        <ChatSelectionItem />
        <ChatSelectionItem />
        <ChatSelectionItem />
        <ChatSelectionItem />
        <ChatSelectionItem />
        <ChatSelectionItem />
        <ChatSelectionItem />
      </ul>
    </div>
  );
}

export default ChatSelection;
