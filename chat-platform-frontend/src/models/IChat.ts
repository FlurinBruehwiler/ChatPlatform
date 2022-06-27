import IMessage from "./IMessage";

export default interface IChat {
    name: string;
    chatId: number;
    messages: IMessage[];
    usernames: string[];
}
