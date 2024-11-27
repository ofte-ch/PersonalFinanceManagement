import { useMutation } from "@tanstack/react-query";
import { api } from "~/axios/api";
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
      setUser(result.user);
      setIsAuthenticated(true);
      onSuccess?.(data, ...args);
    },
    onError: (error, ...args) => {
      onError?.(error, ...args);
      setIsAuthenticated(false);
    },
    ...restConfig,
  });
};