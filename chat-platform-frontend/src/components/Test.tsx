import {SetStateAction, useEffect, useState} from "react";
import {HubConnectionBuilder} from "@microsoft/signalr";
import Message from "../models/Message";
import axios from "../axios";
import AuthUser from "../models/AuthUser";
import User from "../models/User";

function Test(){
    const [ connection, setConnection ] = useState<any | undefined>(undefined);
    const [ messages, setMessages ] = useState<Message[]>([]);
    const [ user, setUser ] = useState<User|undefined>(undefined);

    const tryEstablishConnection = () => {
        const newConnection = new HubConnectionBuilder()
            .withUrl("https://localhost:7087/chatHub")
            .withAutomaticReconnect()
            .build();
        setConnection(newConnection);
    }

    useEffect(() => {
        if (!connection)
            return;
        connection.start({ withCredentials: false })
            .then(() => {
                connection.on("ReceiveMessage", (message: Message) => {
                    console.log(JSON.stringify(message));
                    setMessages(arr => [...arr, message]);
                });
            })
            .catch((e: any) => console.log('Connection failed: ', e));

    }, [connection]);

    let createChat = () => {
        if(!connection)
            return;

        connection.invoke("CreateChat").catch((err : any) => {console.log(JSON.stringify(err))})
    }

    let addUserToChat = (chatId: number, username: string) => {
        if(!connection)
            return;

        connection.invoke("AddUserToChat", chatId, username).catch((err : any) => {console.log(JSON.stringify(err))})
    }

    let removeUserFromChat = (chatId: number, username: string) => {
        if(!connection)
            return;

        connection.invoke("RemoveUserFromChat", chatId, username)
            .catch((error : any) => console.log(error));
    }

    let login = async (username: string, password: string) => {
        await axios
            .post("/login", {username: username, password: password} as AuthUser)
            .catch((error: any) => console.log(error));
    }

    let register = async (username: string, password: string) => {
        await axios
            .post<number>("/register", {username: username, password: password} as AuthUser)
            .catch((error: any) => console.log(error))
            .then(response =>{
                if(!response)
                    return;
                setUser({ username: username, id: response.data } as User)
            });
    }

    return(
        <div>
            <div>
                <h2>Register</h2>
                <div className={"flex"}>
                    <p>Username</p>
                    <input type="text" className={"border-black border-2"}/>
                </div>
                <div className={"flex"}>
                    <p>Password</p>
                    <input type="text" className={"border-black border-2"}/>
                </div>
            </div>
            <div className={"w-full border-2 border-black"}></div>
            <div>
                <h2>Login</h2>
                <div className={"flex"}>
                    <p>Username</p>
                    <input type="text" className={"border-black border-2"}/>
                </div>
                <div className={"flex"}>
                    <p>Password</p>
                    <input type="text" className={"border-black border-2"}/>
                </div>
            </div>
            <div className={"w-full border-2 border-black"}></div>
        </div>
    );
}

export default Test;