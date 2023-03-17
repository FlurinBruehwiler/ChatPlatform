import axios from "axios";

export const baseUrl = "https://webapp-230317093134.azurewebsites.net";

const instance = axios.create({
  baseURL: baseUrl,
  withCredentials: true,
});

export default instance;
