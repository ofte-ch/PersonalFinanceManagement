import { useMutation, useQueryClient } from "@tanstack/react-query";
import { api } from "~/configs/api";

// API để cập nhật giao dịch
export const deleteTransaction = async ({id, ...params}) => {
    return await api.delete(`/GiaoDich/${id}`, params);
};

export const useDeleteTransaction = () => {
    return useMutation({
        mutationFn: deleteTransaction({id}),
        onSuccess: (data) => {
            console.log("Successfully delete transaction. ", data);
        },
        onError: (error) => {
            console.log("Unable to delete transaction. ", error);
        },
    });
}