import { useMutation, useQueryClient } from "@tanstack/react-query";
import { api } from "~/configs/api";

export const createTransaction = ({ id, tenGiaoDich, ngayGiaoDich, loaiGiaoDich, taiKhoan, theLoai, tongTien, ghiChu }) => {
  return api.post(`/accounts`, {
        id,
        tenGiaoDich, 
        ngayGiaoDich, 
        loaiGiaoDich, 
        taiKhoan, 
        theLoai, 
        tongTien, 
        ghiChu,
  });
};

export const useCreateTransaction = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: createTransaction,
    onSuccess: (data) => {
      queryClient.invalidateQueries({
        queryKey: ['GiaoDich'],
      });
      console.log("Successfully add new transaction", data);
    },
    onError: (error) => {
        console.log("Unable to delete transaction. ", error);
    },
  });
};
