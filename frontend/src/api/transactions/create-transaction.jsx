import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useGetTransactions } from "./get-transactions";
import { api } from "~/configs/api";

export const createTransaction = ({ tenGiaoDich,ngayGiaoDich,taiKhoanChuyen,taiKhoanNhan,theLoai,tongTien,ghiChu }) => {
  console.log(theLoai);
  return api.post(`/transactions`, {
     tenGiaoDich,
     ngayGiaoDich,
     taiKhoanChuyen,
     taiKhoanNhan,
     theLoai,
     tongTien,
     ghiChu
  });
};

export const useCreateTransaction = (options = {}) => {
  const { onSuccess, onError, ...restConfig } = options;

  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createTransaction,
    onSuccess: (data, ...args) => {
      queryClient.invalidateQueries({
        queryKey: useGetTransactions.queryKey,
      });
      onSuccess?.(data, ...args);
    },
    onError: (error, ...args) => {
      onError?.(error, ...args);
    },
    ...restConfig,
  });
};
