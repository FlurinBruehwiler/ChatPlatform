import IMessage from "../models/IMessage";
import { HubConnection } from "@microsoft/signalr";
import { tryEstablishConnection } from "./apiService";
import IsLoggedIn from "./userService";
import IChat from "../models/IChat";

class SignalRService {
  private connection: HubConnection | undefined;
  public receivedMessage: ((message: IMessage) => void) | undefined;
  public joinChat: ((chat: IChat) => void) | undefined;

  connect = async () => {
    if (!IsLoggedIn()) return;

    this.connection = tryEstablishConnection();
    await this.connection.start();
    this.connection.on("ReceiveMessage", (message: IMessage) => {
      if (this.receivedMessage) this.receivedMessage(message);
    });
    this.connection.on("JoinChat", (chat: IChat) => {
      if (this.joinChat) this.joinChat(chat);
    });
  };

  sendMessage = async (chatId: number, messageContent: string) => {
    console.log(chatId);
    if (!this.connection) throw "Connection does not exist";
    await this.connection.invoke("SendMessage", chatId, messageContent);
  };

  createChat = async (name: string, users: string[]): Promise<IChat> => {
    if (!this.connection) throw "Connection does not exist";

    const chatId = await this.connection.invoke<number>(
      "CreateChat",
      name,
      users
    );

    return {
      name: name,
      chatId: chatId,
      messages: [] as IMessage[],
      usernames: [""],
    } as IChat;
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

    return await this.connection.invoke<IChat[]>("GetChats");
  };

  addConnectionToGroup = async (chatId: number) => {
    if (!this.connection) return;

    await this.connection.invoke("AddConnectionToGroup", chatId);
  };

  leaveChat = async (chatId: number) => {
    if (!this.connection) return;

    await this.connection.invoke("LeaveChat", chatId);
  };
}

export { SignalRService };
