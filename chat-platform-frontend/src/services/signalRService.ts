import IMessage from "../models/IMessage";
import { HubConnection } from "@microsoft/signalr";
import { tryEstablishConnection } from "./apiService";

class SignalRService {
  private readonly connection: HubConnection;

  constructor() {
    this.connection = tryEstablishConnection();
    this.connection
      .start()
      .then(() => {})
      .catch((e: any) => console.log("Connection failed: ", e));
  }

  createChat = (name: string) => {
    if (!this.connection) return;

    this.connection.invoke("CreateChat", name).catch((err: any) => {
      console.log(JSON.stringify(err));
    });
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
    this.connection.on("ReceiveMessage", (message: IMessage) => {
      console.log(JSON.stringify(message));
      return message;
    });
  };
}

export { SignalRService };
