import { useMutation, useQueryClient } from "@tanstack/react-query";
import { getAccountTypes } from "~/api/account-types/get-account-types";
import { api } from "~/configs/api";

export const deleteAccountTypes = (id) => {
  return api.delete(`/account-types/${id}`);
};

export const useDeleteAccountTypes = (options = {}) => {
  const { onSuccess, onError, ...restConfig } = options;

  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteAccountTypes,
    onSuccess: (data, ...args) => {
      queryClient.invalidateQueries({
        queryKey: getAccountTypes.queryKey,
      });
      onSuccess?.(data, ...args);
    },
    onError: (error, ...args) => {
      onError?.(error, ...args);
    },
    ...restConfig,
  });
};
