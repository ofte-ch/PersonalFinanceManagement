import { useMutation } from "@tanstack/react-query";
import Cookies from "js-cookie";
import { api } from "~/configs/api";
import { useAuthStore } from "~/stores/auth/authStore";

export const login = ({ username, password }) => {
  return api.post(`/auth/login`, {
    username,
    password,
  });
};

export const useLogin = (options = {}) => {
  const { onSuccess, onError, ...restConfig } = options;
  const { setUser, setIsAuthenticated } = useAuthStore((state) => state);

  return useMutation({
    mutationFn: login,
    onSuccess: (data, ...args) => {
      const result = data.data;
      setUser(result);
      Cookies.set("access_token", data.token);
      setIsAuthenticated(true);
      onSuccess?.(data, ...args);
    },
    onError: (error, ...args) => {
      onError?.(error, ...args);
      setIsAuthenticated(false);
      Cookies.remove("access_token");
      console.log(error)
    },
    ...restConfig,
  });
};