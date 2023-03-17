export default interface IMessage {
    messageContent: string;
    username: string;
    chatId: number;
    messageId: number;
    image?: string;
}
