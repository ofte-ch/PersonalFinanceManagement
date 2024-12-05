import { useMutation, useQueryClient } from "@tanstack/react-query";
import { getAccountTypes } from "~/api/account-types/get-account-types";

import { api } from "~/configs/api";

export const updateAccountTypes = (id, data) => {
  return api.put(`/account-types/${id}`, data);
};

export const useUpdateAccountTypes = (options = {}) => {
  const { onSuccess, onError, ...restConfig } = options;

  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({id,data}) => updateAccountTypes(id,data),
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
