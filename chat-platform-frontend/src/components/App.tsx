import ChatSelection from "./ChatSelection";
import React, {useEffect, useState} from "react";
import AuthModal from "./AuthModal";
import {checkIfLoggedIn, logout} from "../services/apiService";

function App() {
    const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);

    useEffect(() => {
        checkIfLoggedIn().then(x => setIsLoggedIn(x));
    }, [])

    if (isLoggedIn) {
        return (
            <ChatSelection Logout={async () => {
                await logout()
                setIsLoggedIn(false);
            }}/>
        );
    } else {
        return <AuthModal CloseCallback={() => setIsLoggedIn(true)}/>
    }

}

export default App;
