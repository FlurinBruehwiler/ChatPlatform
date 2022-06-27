import ChatSelection from "./ChatSelection";
import React, {useState} from "react";
import IsLoggedIn from "../services/userService";
import AuthModal from "./AuthModal";

function App() {
    const [isLoggedIn, setIsLoggedIn] = useState<boolean>(IsLoggedIn());

    if (isLoggedIn) {
        return (
            <ChatSelection Logout={() => setIsLoggedIn(false)}/>
        );
    } else {
        return <AuthModal CloseCallback={() => setIsLoggedIn(true)}/>
    }

}

export default App;
