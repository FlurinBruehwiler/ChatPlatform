import axios from "axios";

const instance = axios.create({
    baseURL: "https://localhost:7087/",
});

export default instance;