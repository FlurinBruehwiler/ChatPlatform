import IMessage from "../models/IMessage";
import { HubConnection } from "@microsoft/signalr";
import {checkIfLoggedIn, tryEstablishConnection} from "./apiService";
import IChat from "../models/IChat";
import IUser from "../models/IUser";

class SignalRService {
  private connection: HubConnection | undefined;
  public receivedMessage: ((message: IMessage) => void) | undefined;
  public receiveInvite: ((chatId: number) => void) | undefined;
  public receiveKick: ((chatId: number) => void) | undefined;

  connect = async () => {
    if (!await checkIfLoggedIn()) return;

    this.connection = tryEstablishConnection();
    await this.connection.start();
    this.connection.on("ReceiveMessage", (message: IMessage) => {
      if (this.receivedMessage) this.receivedMessage(message);
    });
    this.connection.on("ReceiveInvite", (chatId: number) => {
      if (this.receiveInvite) this.receiveInvite(chatId);
    });
    this.connection.on("ReceiveKick", (chatId: number) => {
      if (this.receiveKick) this.receiveKick(chatId);
    });
  };

  sendMessage = async (chatId: number, messageContent: string) => {
    console.log(chatId);
    if (!this.connection) throw "Connection does not exist";
    await this.connection.invoke("SendMessage", chatId, messageContent);
  };

  createChat = async (name: string, users: string[]) => {
    if (!this.connection) throw "Connection does not exist";

    await this.connection.invoke<number>("CreateChat", name, users);
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

  joinChat = async (chatId: number): Promise<IChat> => {
    if (!this.connection) throw "Connection does not exist";

    return await this.connection.invoke<IChat>("JoinChat", chatId);
  };

  leaveChat = async (chatId: number) => {
    if (!this.connection) return;

    await this.connection.invoke("LeaveChat", chatId);
  };

  kickChat = async (chatId: number) => {
    if (!this.connection) throw "Connection does not exist";

    await this.connection.invoke("KickChat", chatId);
  };

  getCurrentUser = async (): Promise<IUser> => {
    if (!this.connection) throw "Connection does not exist";

    return await this.connection.invoke<IUser>("GetCurrentUser");
  };
}

export { SignalRService };
