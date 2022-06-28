import axios from "../axios";
import IDtoUser from "../models/IDtoUser";
import { HubConnectionBuilder } from "@microsoft/signalr";

let login = async (username: string, password: string) => {
  try {
    await axios.post("/login", {
      username: username,
      password: password,
    } as IDtoUser);
  } catch (err) {
    throw JSON.stringify(err);
  }
};

let register = async (username: string, password: string) => {
  try {
    await axios.post("/register", {
      username: username,
      password: password,
    } as IDtoUser);
  } catch (err) {
    throw JSON.stringify(err);
  }
};

const tryEstablishConnection = () => {
  return new HubConnectionBuilder()
    .withUrl("https://localhost:7087/chatHub")
    .withAutomaticReconnect()
    .build();
};

export { login, register, tryEstablishConnection };
