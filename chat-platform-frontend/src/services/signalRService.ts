import IMessage from "../models/IMessage";
import { HubConnection } from "@microsoft/signalr";
import { tryEstablishConnection } from "./apiService";
import IsLoggedIn from "./UserService";

class SignalRService {
  private connection: HubConnection | undefined;

  constructor() {
    this.connect();
  }

  connect = () => {
    if(!IsLoggedIn())
      return;

    this.connection = tryEstablishConnection();
    this.connection
        .start()
        .then(() => {})
        .catch((e: any) => console.log("Connection failed: ", e));
  }

  createChat = async (name: string) : Promise<number> => {
    if (!this.connection) return -1;

    await this.connection.invoke<number>("CreateChat", name).catch((err: any) => {
      console.log(JSON.stringify(err));
      return -1;
    }).then((result) => {
      if(result)
        return result;
    });

    return -1;
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

  ReceiveMessage = () => {
    if (!this.connection) return;

    this.connection.on("ReceiveMessage", (message: IMessage) => {
      console.log(JSON.stringify(message));
      return message;
    });
  };
}

export { SignalRService };
