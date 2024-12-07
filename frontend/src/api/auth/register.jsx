import { api } from "~/configs/api";
import { useMutation } from "@tanstack/react-query";

export const register = ({ name, username, password }) => {
  return api.post(`/auth/register`, {
    name,
    username,
    password,
  });
};

export const useRegister = (options = {}) => {
  const { onSuccess, onError, ...restConfig } = options;

  return useMutation({
    mutationFn: register,
    onSuccess: (data, ...args) => {
      onSuccess?.(data, ...args);
    },
    onError: (error, ...args) => {
      onError?.(error, ...args);
    },
    ...restConfig,
  });
};
