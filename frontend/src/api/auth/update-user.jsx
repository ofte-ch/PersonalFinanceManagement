import { api } from "~/configs/api";
import { useMutation } from "@tanstack/react-query";

export const updateUser = ({ userId,name, oldPassword, newPassword }) => {
  return api.post(`/auth/update-user`, {
    userId,
    name,
    oldPassword,
    newPassword,
  });
};

export const useUpdateUser = (options = {}) => {
  const { onSuccess, onError, ...restConfig } = options;

  return useMutation({
    mutationFn: updateUser,
    onSuccess: (data, ...args) => {
      onSuccess?.(data, ...args);
    },
    onError: (error, ...args) => {
      onError?.(error, ...args);
    },
    ...restConfig,
  });
};
