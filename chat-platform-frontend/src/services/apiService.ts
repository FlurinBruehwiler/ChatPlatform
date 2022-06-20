import axios from "../axios";
import IDtoUser from "../models/IDtoUser";
import { HubConnectionBuilder } from "@microsoft/signalr";
import IApiError from "../models/IApiError";

let login = async (
  username: string,
  password: string,
  callback: (result: IApiError) => void
) => {
  await axios
    .post("/login", {
      username: username,
      password: password,
    } as IDtoUser)
    .catch((error) => {
      callback(error as IApiError);
    });
};

let register = async (
  username: string,
  password: string,
  callback: (result: IApiError) => void
) => {
  await axios
    .post("/register", {
      username: username,
      password: password,
    } as IDtoUser)
    .catch((error) => {
      callback(error as IApiError);
    });
};

const tryEstablishConnection = () => {
  return new HubConnectionBuilder()
    .withUrl("https://localhost:7087/chatHub")
    .withAutomaticReconnect()
    .build();
};

export { login, register, tryEstablishConnection };
