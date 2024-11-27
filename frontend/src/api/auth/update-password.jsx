import { api } from "~/config/api";
import { useMutation } from "@tanstack/react-query";

export const updatePassword = ({ userId, password, newPassword }) => {
  return api.post(`/auth/update-password`, {
    userId,
    password,
    newPassword,
  });
};

export const useUpdatePassword = (options = {}) => {
  const { onSuccess, onError, ...restConfig } = options;

  return useMutation({
    mutationFn: updatePassword,
    onSuccess: (data, ...args) => {
      onSuccess?.(data, ...args);
    },
    onError: (error, ...args) => {
      onError?.(error, ...args);
    },
    ...restConfig,
  });
};
