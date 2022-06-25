import IMessage from "./IMessage";

export default interface IChat {
  name: string;
  chatdId: number;
  messages: IMessage[];
  usernames: string[];
}
