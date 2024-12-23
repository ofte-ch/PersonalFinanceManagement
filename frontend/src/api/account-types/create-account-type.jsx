import { useMutation, useQueryClient } from "@tanstack/react-query";
import { getAccountTypes } from "~/api/account-types/get-account-types";

import { api } from "~/configs/api";

export const createAccountType = ({ Ten }) => {
  return api.post(`/account-types`, {
      Ten
  });
};

export const useCreateAccountType = (options = {}) => {
  const { onSuccess, onError, ...restConfig } = options;

  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createAccountType,
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
