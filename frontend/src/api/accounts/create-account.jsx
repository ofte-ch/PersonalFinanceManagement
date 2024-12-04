import { useMutation, useQueryClient } from "@tanstack/react-query";
import { getAccountsQueryOptions } from "~/api/accounts/get-accounts";
import { api } from "~/configs/api";

export const createAccount = ({ tenTaiKhoan, loaiTaiKhoanId, soDu }) => {
  return api.post(`/accounts`, {
      tenTaiKhoan,
      loaiTaiKhoanId,
      soDu,
  });
};

export const useCreateAccount = (options = {}) => {
  const { onSuccess, onError, ...restConfig } = options;

  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createAccount,
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
