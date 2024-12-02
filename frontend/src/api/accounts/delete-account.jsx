import { useMutation, useQueryClient } from "@tanstack/react-query";
import { getAccountsQueryOptions } from "~/api/accounts/get-accounts";
import { api } from "~/configs/api";

export const deleteAccount = (id) => {
  return api.delete(`/account/${id}`);
};

export const useDeleteAccount = (options = {}) => {
  const { onSuccess, onError, ...restConfig } = options;

  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteAccount,
    onSuccess: (data, ...args) => {
      queryClient.invalidateQueries({
        queryKey: getAccountsQueryOptions.queryKey,
      });
      onSuccess?.(data, ...args);
    },
    onError: (error, ...args) => {
      onError?.(error, ...args);
    },
    ...restConfig,
  });
};
