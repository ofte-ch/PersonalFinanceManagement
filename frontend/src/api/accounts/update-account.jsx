import { useMutation, useQueryClient } from "@tanstack/react-query";
import { getAccountsQueryOptions } from "~/api/accounts/get-accounts";
import { api } from "~/configs/api";

export const updateAccount = (id, data) => {
  return api.put(`/accounts/${id}`, data);
};

export const useUpdateAccount = (options = {}) => {
  const { onSuccess, onError, ...restConfig } = options;

  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({id,data}) => updateAccount(id,data),
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
