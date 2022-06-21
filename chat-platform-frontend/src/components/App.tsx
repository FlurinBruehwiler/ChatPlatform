import TestChats from "./examples/TestChats";
import { useEffect } from "react";

function App() {
  useEffect(() => {
    console.log("app ctor");
  }, []);

  return (
    <div className={"grid grid-cols-main h-full"}>
      <TestChats />
    </div>
  );
}

export default App;
