function Chat() {
  return (
    <div className={"w-full bg-gray-200 flex flex-col justify-between"}>
      <div className={"bg-gray-400 flex flex-row p-3 items-center"}>
        <img
          src="https://randomuser.me/api/portraits/women/40.jpg"
          alt=""
          className={"h-14 w-14 rounded-full"}
        />
        <div className={"text-2xl ml-4"}>Name</div>
      </div>
      <div className={"bg-gray-200 h-full"}>
        <ul></ul>
      </div>
      <div className={"bg-gray-600 flex flex-row"}>
        <input
          type="text"
          className={"w-full m-1 h-9 m-2 p-4 rounded-2xl focus:border-gray-800"}
        />
        <button className={"rounded-full bg-gray-400 w-11 h-11"}></button>
      </div>
    </div>
  );
}

export default Chat;
