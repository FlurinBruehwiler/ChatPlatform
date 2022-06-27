import IMessage from "../models/IMessage";
import {HubConnection} from "@microsoft/signalr";
import {tryEstablishConnection} from "./apiService";
import IsLoggedIn from "./userService";
import IChat from "../models/IChat";

class SignalRService {
    private connection: HubConnection | undefined;

    connect = async () => {
        if (!IsLoggedIn())
            return;

        this.connection = tryEstablishConnection();
        await this.connection.start();
    }

    createChat = async (name: string): Promise<IChat> => {
        if (!this.connection) throw "Connection does not exist";

        const chatId = await this.connection.invoke<number>("CreateChat", name)

        return {name: name, chatId: chatId, messages: [] as IMessage[], usernames: [""]} as IChat;
    };

    addUserToChat = (chatId: number, username: string) => {
        if (!this.connection) return;

        this.connection
            .invoke("AddUserToChat", chatId, username)
            .catch((err: any) => {
                console.log(JSON.stringify(err));
            });
    };

    removeUserFromChat = (chatId: number, username: string) => {
        if (!this.connection) return;

        this.connection
            .invoke("RemoveUserFromChat", chatId, username)
            .catch((error: any) => console.log(error));
    };

    getChats = async (): Promise<IChat[]> => {
        if (!this.connection) throw "Connection does not exist";

        const chats = await this.connection.invoke<IChat[]>("GetChats")
        return chats;
    }

    ReceiveMessage = () => {
        if (!this.connection) return;

        this.connection.on("ReceiveMessage", (message: IMessage) => {
            console.log(JSON.stringify(message));
            return message;
        });
    };
}

export {SignalRService};
