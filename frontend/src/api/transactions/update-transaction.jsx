import { useMutation, useQueryClient } from "@tanstack/react-query";
import { api } from "~/configs/api";

// API để cập nhật giao dịch
export const updateTransaction = async ({id, data}) => {
    return api.put(`/transactions/${id}`, data);
};

export const useUpdateTransaction = () => {
    return useMutation({
        mutationFn: ({id, data}) => updateTransaction({id, data}),
        onSuccess: (data) => {
            console.log("Successfully updated transaction. ", data);
        },
        onError: (error) => {
            console.log("Unable to update transaction. ", error);
        },
    });
}