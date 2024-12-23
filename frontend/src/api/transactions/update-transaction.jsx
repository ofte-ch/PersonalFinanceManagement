import { useMutation, useQueryClient } from "@tanstack/react-query";
import { api } from "~/configs/api";
import { getTransactionsQueryOptions } from "./get-transactions";
import { getAccountsQueryOptions } from "../accounts/get-accounts";

// API để cập nhật giao dịch
export const updateTransaction = async ({id, data}) => {
    return api.put(`/transactions/${id}`, data);
};

export const useUpdateTransaction = (options = {}) => {
    const { onSuccess, onError, ...restConfig } = options;

    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: ({id,data}) => updateTransaction({id,data}),
        onSuccess: (data, ...args) => {
          queryClient.invalidateQueries({
            queryKey: getTransactionsQueryOptions.queryKey,
          });
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
}