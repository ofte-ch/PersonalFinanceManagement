import Axios from "axios";
import { BACKEND_ENDPOINT } from "~/configs/env";

export const api = Axios.create({
  baseURL: BACKEND_ENDPOINT,
});
api.interceptors.response.use(
     (response) => {
       return response.data;
     },
     (error) => {
       return Promise.reject(error);
     }
   );
   