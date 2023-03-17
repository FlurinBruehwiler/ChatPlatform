import axios from "axios";

export const baseUrl = "https://localhost:7087";

const instance = axios.create({
  baseURL: baseUrl,
  withCredentials: true,
});

export default instance;
