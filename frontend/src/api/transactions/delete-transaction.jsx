import { useMutation } from "@tanstack/react-query";
import { api } from "~/configs/api";

// API để cập nhật giao dịch
export const deleteTransaction = async ({id, ...params}) => {
    const response = await api.put(`/GiaoDich/${id}`, params);
    return response.data;
};

export const useDeleteTransaction = () => {
    return useMutation(deleteTransaction);
}