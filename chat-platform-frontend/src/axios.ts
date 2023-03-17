import axios from "axios";

export const baseUrl = "https://chatplatform-production-5907.up.railway.app";

const instance = axios.create({
  baseURL: baseUrl,
  withCredentials: true,
});

export default instance;
