import { useMutation, useQueryClient } from "@tanstack/react-query";
import { getAccountTypes } from "~/api/account-types/get-account-types";

import { api } from "~/configs/api";

export const updateAccountType = (id, data) => {
  return api.put(`/account-types/${id}`, data);
};

export const useUpdateAccountType = (options = {}) => {
  const { onSuccess, onError, ...restConfig } = options;

  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({id, data}) => updateAccountType(id, data),
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
