import axios from "../axios";
import IDtoUser from "../models/IDtoUser";
import { HubConnectionBuilder } from "@microsoft/signalr";
import IApiError from "../models/IApiError";

let login = async (
  username: string,
  password: string,
) => {
    try {
        await axios
            .post("/login", {
                username: username,
                password: password,
            } as IDtoUser)
    }catch(err){
        throw "Login failed";
    }
};

let register = async (
  username: string,
  password: string,
) => {
  await axios
    .post("/register", {
      username: username,
      password: password,
    } as IDtoUser);
};

const tryEstablishConnection = () => {
  return new HubConnectionBuilder()
    .withUrl("https://localhost:7087/chatHub")
    .withAutomaticReconnect()
    .build();
};

export { login, register, tryEstablishConnection };
