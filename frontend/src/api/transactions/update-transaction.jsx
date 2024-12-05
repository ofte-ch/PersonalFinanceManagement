import { useMutation } from "@tanstack/react-query";
import { api } from "~/configs/api";

// API để cập nhật giao dịch
export const updateTransaction = async ({id, ...params}) => {
    const response = await api.put(`/GiaoDich/${id}`, params);
    return response.data;
};

export const useUpdateTransaction = () => {
    return useMutation(updateTransaction, {
        onSuccess: (data) => {
            console.log("Successfully updated transaction:", data);
        },
        onError: (error) => {
            console.log("Error updating transaction: ", error);
        }
    });
}