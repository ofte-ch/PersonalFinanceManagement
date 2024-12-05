import { useMutation, useQueryClient } from "@tanstack/react-query";
import { api } from "~/configs/api";

// API để cập nhật giao dịch
export const deleteTransaction = (id) => {
    return api.delete(`/GiaoDich/${id}`);
};

export const useDeleteTransaction = () => {
  const queryClient = useQueryClient();
  return useMutation({
        mutationFn: deleteTransaction,
        onSuccess: (data) => {
            queryClient.invalidateQueries({
                queryKey:  ['GiaoDich'],
            });
            console.log("Successfully delete transaction. ", data);
        },
        onError: (error) => {
            console.log("Unable to delete transaction. ", error);
        },
    });
}