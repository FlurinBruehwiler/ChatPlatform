function ChatSelectionItem() {
  return (
    <li
      className={
        "p-3 bg-gray-400 rounded-2xl h-20 flex items-center mb-3 cursor-pointer"
      }
    >
      <img
        src="https://randomuser.me/api/portraits/women/40.jpg"
        alt=""
        className={"h-14 w-14 rounded-full"}
      />
      <div className={"w-full ml-4"}>
        <div>Name</div>
        <div className={""}>online</div>
      </div>
    </li>
  );
}
export default ChatSelectionItem;
