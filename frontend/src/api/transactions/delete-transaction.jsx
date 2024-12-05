import { useMutation, useQueryClient } from "@tanstack/react-query";
import { getAccountsQueryOptions } from "~/api/accounts/get-accounts";
import { api } from "~/configs/api";

export const deleteTransaction = (id) => {
  return api.delete(`/transactions/${id}`);
};

export const useDeleteTransaction = (options = {}) => {
  const { onSuccess, onError, ...restConfig } = options;

  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteTransaction,
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
