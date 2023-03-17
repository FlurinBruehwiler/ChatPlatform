import axios from "../axios";
import IAuthUser from "../models/IAuthUser";
import { HubConnectionBuilder } from "@microsoft/signalr";
import {AxiosError} from "axios";

let login = async (username: string, password: string) : Promise<void | string> => {
  try {
    await axios.post("/login", {
      username: username,
      password: password,
    } as IAuthUser);
  } catch (err) {
    console.log(err);
    // @ts-ignore
    return err.response.data.errorMessage;
  }
};

let register = async (username: string, password: string) : Promise<void | string> => {
  try {
    await axios.post("/register", {
      username: username,
      password: password,
    } as IAuthUser);
  } catch (err) {
    // @ts-ignore
    return err.response.data.errorMessage;
  }
};

let logout = async () => {
  try {
    await axios.get("/logout");
  }catch (err){
    throw err;
  }
}

let checkIfLoggedIn = async () => {
  try {
    await axios.get("/protected");
  }catch (err){
    return false;
  }
  return true;
}

const tryEstablishConnection = () => {
  return new HubConnectionBuilder()
    .withUrl("https://localhost:7087/chatHub")
    .withAutomaticReconnect()
    .build();
};

export { checkIfLoggedIn, logout, login, register, tryEstablishConnection };
